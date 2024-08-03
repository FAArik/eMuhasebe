using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using eMuhasebeApi.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeApi.Infrastructure.Repositories;

internal sealed class CashRegisterRepository : Repository<CashRegister, CompanyDbContext>, ICashRegisterRepository
{
    public CashRegisterRepository(CompanyDbContext context) : base(context)
    {
    }
}
