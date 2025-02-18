using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using eMuhasebeApi.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeApi.Infrastructure.Repositories;

internal sealed class CustomerDetailRepository:Repository<CustomerDetail,CompanyDbContext>, ICustomerDetailRepository
{
    public CustomerDetailRepository(CompanyDbContext context) : base(context)
    {
    }
}