using AutoMapper;
using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Events;
using eMuhasebeApi.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(
    UserManager<AppUser> userManager,
    IMapper mapper,
    IMediator mediator,
    ICompanyUserRepository companyUserRepository,
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<UpdateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        AppUser? appUser = await userManager.Users.Where(x => x.Id == request.Id).Include(x => x.CompanyUsers).FirstOrDefaultAsync();
        bool isMailChanged = false;
        if (appUser == null)
        {
            return Result<string>.Failure("Kullanıcı Bulunamadı");
        }
        if (appUser.UserName != request.UserName)
        {
            bool isUserNameExist = await userManager.Users.AnyAsync(x => x.UserName == request.UserName, cancellationToken);
            if (isUserNameExist)
            {
                return Result<string>.Failure("Bu kullanıcı adı daha önce kullanılmış");
            }
        }
        if (appUser.Email != request.Email)
        {
            bool isEmailExist = await userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
            if (isEmailExist)
            {
                return Result<string>.Failure("Bu mail adresi daha önce kullanılmış");
            }
            isMailChanged = true;
            appUser.EmailConfirmed = false;

        }

        mapper.Map(request, appUser);

        IdentityResult identityResult = await userManager.UpdateAsync(appUser);

        if (!identityResult.Succeeded)
        {
            return Result<string>.Failure(identityResult.Errors.Select(x => x.Description).ToList());
        }
        if (request.Password != null)
        {
            string Token = await userManager.GeneratePasswordResetTokenAsync(appUser);
            identityResult = await userManager.ResetPasswordAsync(appUser, Token, request.Password);
        }
        if (!identityResult.Succeeded)
        {
            return Result<string>.Failure(identityResult.Errors.Select(x => x.Description).ToList());
        }
        companyUserRepository.DeleteRange(appUser.CompanyUsers);
        List<CompanyUser> companyUsers = request.CompanyIds.Select(x => new CompanyUser
        {
            AppUserId = appUser.Id,
            CompanyId = x
        }
        ).ToList();
        await companyUserRepository.AddRangeAsync(companyUsers);
        await unitOfWork.SaveChangesAsync();

        cacheService.Remove("users");

        if (isMailChanged)
        {
            //todo tekrar mail onayı
            await mediator.Publish(new AppUserEvent(appUser.Id));
        }

        return "Kullanıcı başarıyla güncellendi";
    }
}
