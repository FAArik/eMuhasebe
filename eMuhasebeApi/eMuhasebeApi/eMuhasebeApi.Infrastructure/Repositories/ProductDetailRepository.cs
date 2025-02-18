using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using eMuhasebeApi.Infrastructure.Context;
using GenericRepository;

namespace eMuhasebeApi.Infrastructure.Repositories;

internal sealed class ProductDetailRepository:Repository<ProductDetail,CompanyDbContext>,IProductDetailRepository
{
    public ProductDetailRepository(CompanyDbContext context) : base(context)
    {
    }
}