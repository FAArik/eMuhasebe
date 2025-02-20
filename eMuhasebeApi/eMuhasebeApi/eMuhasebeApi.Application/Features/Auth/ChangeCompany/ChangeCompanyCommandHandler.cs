using eMuhasebeApi.Application.Features.Auth.Login;
using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Auth.ChangeCompany;

sealed class ChangeCompanyCommandHandler(
    ICompanyUserRepository companyUserRepository,
    UserManager<AppUser> userManager,
    IHttpContextAccessor httpContextAccessor,
    IJwtProvider jwtProvider,
    ICacheService cacheService
) : IRequestHandler<ChangeCompanyCommand, Result<LoginCommandResponse>>
{
    public async Task<Result<LoginCommandResponse>> Handle(ChangeCompanyCommand request,
        CancellationToken cancellationToken)
    {
        if (httpContextAccessor.HttpContext is null)
        {
            return Result<LoginCommandResponse>.Failure("Bu işlemi yapmaya yetkiniz yok");
        }

        string? userId = httpContextAccessor.HttpContext.User.FindFirstValue("Id");
        if (string.IsNullOrEmpty(userId))
        {
            return Result<LoginCommandResponse>.Failure("Bu işlemi yapmaya yetkiniz yok");
        }

        AppUser? appuser = await userManager.FindByIdAsync(userId);
        if (appuser == null)
        {
            return Result<LoginCommandResponse>.Failure("Kullanıcı bulunamadı ");
        }

        List<CompanyUser> companyUsers = await companyUserRepository.Where(x => x.AppUserId == appuser.Id)
            .Include(x => x.Company).ToListAsync(cancellationToken);
        List<Company> companies = companyUsers.Select(x => new Company
        {
            Id = x.CompanyId,
            Name = x.Company!.Name,
            TaxDepartment = x.Company!.TaxDepartment,
            TaxNumber = x.Company!.TaxNumber,
            FullAddress = x.Company!.FullAddress,
        }).ToList();

        var response = await jwtProvider.CreateToken(appuser, request.CompanyId, companies);

        cacheService.RemoveAll();

        return response;
    }
}