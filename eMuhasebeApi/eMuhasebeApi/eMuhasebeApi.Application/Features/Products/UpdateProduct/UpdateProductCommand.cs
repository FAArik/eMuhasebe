using AutoMapper;
using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Products.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    string Name
) : IRequest<Result<string>>;

internal sealed class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService,
    IMapper mapper
) : IRequestHandler<UpdateProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product =
            await productRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.Id, cancellationToken);
        if (product is null)
        {
            return Result<string>.Failure("Ürün bulunamadı");
        }

        if (product.Name != request.Name)
        {
            bool isNameExist = await productRepository.AnyAsync(x => x.Name == request.Name, cancellationToken);
            if (isNameExist)
            {
                return Result<string>.Failure("Bu ürün adı daha önce kullanılmış");
            }
        }
        mapper.Map(request, product);
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
        cacheService.Remove("products");

        return "Ürün kaydı başarıyla güncellendi";
    }
}