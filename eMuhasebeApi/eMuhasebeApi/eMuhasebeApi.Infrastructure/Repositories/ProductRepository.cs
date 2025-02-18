using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using eMuhasebeApi.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeApi.Infrastructure.Repositories;

internal sealed class ProductRepository : Repository<Product, CompanyDbContext>, IProductRepository
{
    public ProductRepository(CompanyDbContext context) : base(context)
    {
    }
}