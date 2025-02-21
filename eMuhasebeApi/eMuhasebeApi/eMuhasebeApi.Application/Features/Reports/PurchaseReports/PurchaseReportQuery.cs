using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Reports.PurchaseReports;

public sealed record PurchaseReportQuery() : IRequest<Result<PurchaseReportQueryResponse>>;

public sealed record PurchaseReportQueryResponse
{
    public List<DateOnly> Dates { get; set; } = new();
    public List<decimal> Amount { get; set; } = new();
}

internal sealed class PurchaseReportQueryHandler(
    IInvoiceRepository invoiceRepository
) : IRequestHandler<PurchaseReportQuery, Result<PurchaseReportQueryResponse>>
{
    public async Task<Result<PurchaseReportQueryResponse>> Handle(PurchaseReportQuery request,
        CancellationToken cancellationToken)
    {
        List<Invoice> invoices = await invoiceRepository.Where(x => x.Type == 2).OrderBy(x => x.Date)
            .ToListAsync(cancellationToken);
        PurchaseReportQueryResponse res = new();
        res.Dates = invoices.GroupBy(x => x.Date).Select(x => x.Key).ToList();
        res.Amount = invoices.GroupBy(x => x.Date).Select(x => x.Sum(x => x.Amount)).ToList();
        return res;
    }
}