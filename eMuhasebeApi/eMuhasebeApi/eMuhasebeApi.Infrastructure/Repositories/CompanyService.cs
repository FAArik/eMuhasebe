using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eMuhasebeApi.Infrastructure.Repositories;

public sealed class CompanyService:ICompanyService
{
    public void MigrateAll(List<Company> companies)
    {
        foreach (var company in companies)
        {
            CompanyDbContext dbContext = new(company);
            dbContext.Database.Migrate();
        }
    }
}