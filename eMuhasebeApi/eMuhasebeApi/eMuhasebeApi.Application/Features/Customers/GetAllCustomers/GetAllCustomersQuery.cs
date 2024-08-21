using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eMuhasebeApi.Application.Features.Customers.GetAllCustomers;

public sealed record GetAllCustomersQuery():IRequest<List<Customer>>;
internal sealed class GetAllCustomersQueryHandler(
    ICustomerRepository customerRepository,
    ICacheService cacheService) : IRequestHandler<GetAllCustomersQuery, List<Customer>>
{
    public async Task<List<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        List<Customer>? customers = cacheService.Get<List<Customer>>("customers"); 
        if (customers is null)
        {
            customers = await customerRepository.GetAll().OrderBy(x => x.Name).ToListAsync();

            cacheService.Set("customers",customers);
        }
        return customers;
    }
}
