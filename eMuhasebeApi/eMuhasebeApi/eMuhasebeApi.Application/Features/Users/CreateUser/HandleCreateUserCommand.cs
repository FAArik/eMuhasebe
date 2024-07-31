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

namespace eMuhasebeApi.Application.Features.Users.CreateUser;

internal sealed class HandleCreateUserCommand(UserManager<AppUser> userManager,
    IMapper mapper,
    IMediator mediator,
    ICompanyUserRepository companyUserRepository,
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<CreateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        bool isUserNameExist = await userManager.Users.AnyAsync(x => x.UserName == request.UserName, cancellationToken);
        if (isUserNameExist)
        {
            return Result<string>.Failure("Bu kullanıcı adı daha önce kullanılmış");
        }
        bool isEmailExist = await userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (isEmailExist)
        {
            return Result<string>.Failure("Bu mail adresi daha önce kullanılmış");
        }

        AppUser appUser = mapper.Map<AppUser>(request);
        IdentityResult identityResult = await userManager.CreateAsync(appUser, request.Password);
        if (!identityResult.Succeeded)
        {
            return Result<string>.Failure(identityResult.Errors.Select(x => x.Description).ToList());
        }
        if (request.CompanyIds != null)
        {
            List<CompanyUser> companyUsers = request.CompanyIds.Select(x => new CompanyUser
            {
                AppUserId = appUser.Id,
                CompanyId = x
            }).ToList();
            await companyUserRepository.AddRangeAsync(companyUsers, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        cacheService.Remove("users");

        await mediator.Publish(new AppUserEvent(appUser.Id));

        return "Kullanıcı kaydı başarıyla tamamlandı";
    }
}