using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using eMuhasebeApi.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeApi.Infrastructure.Repositories;

internal class CashRegisterDetailRepository : Repository<CashRegisterDetail, CompanyDbContext>, ICashRegisterDetailRepository
{
    public CashRegisterDetailRepository(CompanyDbContext context) : base(context)
    {
    }
}
