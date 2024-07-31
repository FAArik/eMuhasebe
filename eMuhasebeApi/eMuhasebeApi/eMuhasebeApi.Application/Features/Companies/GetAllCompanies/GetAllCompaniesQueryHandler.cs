using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Companies.GetAllCompanies;

internal sealed class GetAllCompaniesQueryHandler(ICompanyRepository companyRepository, ICacheService cacheService) : IRequestHandler<GetAllCompaniesQuery, Result<List<Company>>>
{
    public async Task<Result<List<Company>>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        List<Company>? companies;

        companies = cacheService.Get<List<Company>>("companies");

        if (companies is null)
        {

            companies = await companyRepository
                .GetAll()
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            cacheService.Set("companies", companies);
        }

        return companies;
    }
}
