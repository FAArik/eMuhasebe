using System.Windows.Input;
using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Products.DeleteProductById;

public sealed record DeleteProductByIdCommand(
    Guid Id
) : IRequest<Result<string>>;

internal sealed class DeleteProductByIdCommandHandler(
    ICacheService cacheService,
    IProductRepository productRepository,
    IUnitOfWorkCompany unitOfWorkCompany
) : IRequestHandler<DeleteProductByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        Product? product =
            await productRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.Id, cancellationToken);
        if (product is null)
        {
            return Result<string>.Failure("Ürün bulunamadı");
        }

        product.isDeleted = true;
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
        cacheService.Remove("products");

        return "Ürün kaydı başarıyla silindi";
    }
}