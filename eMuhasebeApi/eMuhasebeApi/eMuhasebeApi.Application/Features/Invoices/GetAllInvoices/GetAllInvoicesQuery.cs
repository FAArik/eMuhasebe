using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Invoices.GetAllInvoices;

public sealed record GetAllInvoicesQuery(
) : IRequest<Result<List<Invoice>>>;

internal sealed class GetAllInvoicesQueryHandler(
    IInvoiceRepository invoiceRepository,
    ICacheService cacheService
) : IRequestHandler<GetAllInvoicesQuery, Result<List<Invoice>>>
{
    public async Task<Result<List<Invoice>>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
    {
        List<Invoice>? invoices;
        string key = "invoices";

        invoices = cacheService.Get<List<Invoice>>(key);

        if (invoices is null)
        {
            invoices = await invoiceRepository.GetAll()
                .Include(x => x.Customer)
                .Include(x => x.Details!)
                .ThenInclude(x => x.Product).OrderBy(x => x.Date)
                .ToListAsync(cancellationToken);
            cacheService.Set(key, invoices);
        }

        return invoices;
    }
}