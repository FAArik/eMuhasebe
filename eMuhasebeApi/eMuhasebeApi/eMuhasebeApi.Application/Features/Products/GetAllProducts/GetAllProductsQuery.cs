using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Products.GetAllProducts;

public sealed record GetAllProductsQuery() : IRequest<Result<List<Product>>>;

internal sealed class GetAllProductsQueryHandler(
    IProductRepository productRepository,
    ICacheService cacheService
) : IRequestHandler<GetAllProductsQuery, Result<List<Product>>>
{
    public async Task<Result<List<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        List<Product>? products = cacheService.Get<List<Product>>("products");
        if (products is null)
        {
            products = await productRepository.GetAll().OrderBy(x => x.Name).ToListAsync(cancellationToken);
            
            cacheService.Set("products", products);
        }

        return products;
    }
}