using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using eMuhasebeApi.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeApi.Infrastructure.Repositories;

public sealed class CompanyUserRepository : Repository<CompanyUser, ApplicationDbContext>, ICompanyUserRepository
{
    public CompanyUserRepository(ApplicationDbContext context) : base(context)
    {
    }
}
