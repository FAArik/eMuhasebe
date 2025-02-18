using AutoMapper;
using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Products.CreateProduct;

public sealed record CreateProductCommand(
    string Name
) : IRequest<Result<string>>;

internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWorkCompany unitOfWork,
    ICacheService cacheService,
    IMapper mapper
) : IRequestHandler<CreateProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        bool isNameExist = await productRepository.AnyAsync(x => x.Name == request.Name);
        if (isNameExist)
        {
            return Result<string>.Failure("Bu ürün adı daha önce kullanılmış");
        }

        Product product = mapper.Map<Product>(request);
        await productRepository.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        cacheService.Remove("products");
        
        return "Ürün kaydı başarıyla tamamlandı";
    }
}