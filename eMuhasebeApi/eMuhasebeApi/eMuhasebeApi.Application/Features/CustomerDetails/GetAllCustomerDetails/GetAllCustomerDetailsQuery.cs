using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeApi.Application.Features.CustomerDetails.GetAllCustomerDetails;

public sealed record GetAllCustomerDetailsQuery(
    Guid CustomerId
) : IRequest<Result<Customer>>;

internal sealed class GetAllCustomerDetailsQueryHandler(
    ICustomerRepository customerRepository
) : IRequestHandler<GetAllCustomerDetailsQuery, Result<Customer>>
{
    public async Task<Result<Customer>> Handle(GetAllCustomerDetailsQuery request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.Where(x=>x.Id==request.CustomerId).Include(x=>x.Details).FirstOrDefaultAsync(cancellationToken);
        if (customer==null)
        {
            return Result<Customer>.Failure("Cari Bulunamadı");
        }

        return customer;
    }
}