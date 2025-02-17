using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Customers.GetAllCustomers;

public sealed record GetAllCustomersQuery() : IRequest<Result<List<Customer>>>;

internal sealed class GetAllCustomersQueryHandler(
    ICustomerRepository customerRepository,
    ICacheService cacheService
) : IRequestHandler<GetAllCustomersQuery, Result<List<Customer>>>
{
    public async Task<Result<List<Customer>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        List<Customer>? customers = cacheService.Get<List<Customer>>("customers");
        if (customers is null)
        {
            customers = await customerRepository.GetAll().OrderBy(x => x.Name).ToListAsync(cancellationToken);

            cacheService.Set("customers", customers);
        }

        return customers;
    }
}