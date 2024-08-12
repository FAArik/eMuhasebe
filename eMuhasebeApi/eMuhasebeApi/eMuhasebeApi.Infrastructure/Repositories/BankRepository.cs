using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using eMuhasebeApi.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeApi.Infrastructure.Repositories;

internal sealed class BankRepository : Repository<Bank, CompanyDbContext>, IBankRepository
{
    public BankRepository(CompanyDbContext context) : base(context)
    {
    }
}
