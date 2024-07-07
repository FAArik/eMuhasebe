using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Companies.MigrateAllCompanies;

public sealed record MigrateAllCompaniesCommand():IRequest<Result<string>>;

internal sealed class MigrateAllCompaniesCommandHandler(ICompanyRepository _companyRepository,ICompanyService _companyService) : IRequestHandler<MigrateAllCompaniesCommand, Result<string>>
{
    public async Task<Result<string>> Handle(MigrateAllCompaniesCommand request, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.GetAll().ToListAsync();
        _companyService.MigrateAll(companies);
        return "Tüm şirket databaseleri başarıyla güncellendi!";
    }
}
