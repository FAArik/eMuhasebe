using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using eMuhasebeApi.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeApi.Infrastructure.Repositories;

internal sealed class CustomerRepository : Repository<Customer, CompanyDbContext>, ICustomerRepository
{
    public CustomerRepository(CompanyDbContext context) : base(context)
    {
    }
}
