using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Customers.DeleteCustomerById;

public sealed record DeleteCustomerByIdCommand(Guid Id) : IRequest<Result<string>>;

internal sealed class DeleteCustomerByIdCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService
) : IRequestHandler<DeleteCustomerByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteCustomerByIdCommand request, CancellationToken cancellationToken)
    {
        Customer customer =
            await customerRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.Id, cancellationToken);
        if (customer == null)
        {
            return Result<string>.Failure("Cari bulunamadı");
        }

        customer.isDeleted = true;
        await unitOfWorkCompany.SaveChangesAsync();
        cacheService.Remove("customers");

        return "Cari başarıyla silindi";
    }
}