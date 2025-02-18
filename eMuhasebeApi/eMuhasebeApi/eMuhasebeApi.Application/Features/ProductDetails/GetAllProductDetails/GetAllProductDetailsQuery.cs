using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeApi.Application.Features.ProductDetails.GetAllProductDetails;

public sealed record GetAllProductDetailsQuery(
    Guid ProductId
) : IRequest<Result<Product>>;

internal sealed class GetAllProductDetailsQueryHandler(
    IProductRepository productRepository
) : IRequestHandler<GetAllProductDetailsQuery, Result<Product>>
{
    public async Task<Result<Product>> Handle(GetAllProductDetailsQuery request, CancellationToken cancellationToken)
    {
        Product? product = await productRepository.Where(x => x.Id == request.ProductId).Include(x => x.Details)
            .FirstOrDefaultAsync(cancellationToken);
        if (product is null)
        {
            return Result<Product>.Failure("Ürün bulunamadı");
        }

        return product;
    }
}