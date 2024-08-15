using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using eMuhasebeApi.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeApi.Infrastructure.Repositories;

internal sealed class BankDetailRepository : Repository<BankDetail, CompanyDbContext>, IBankDetailRepository
{
    public BankDetailRepository(CompanyDbContext context) : base(context)
    {
    }
}
