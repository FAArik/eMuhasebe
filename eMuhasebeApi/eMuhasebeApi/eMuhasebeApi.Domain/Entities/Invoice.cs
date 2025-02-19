using eMuhasebeApi.Domain.Abstractions;
using eMuhasebeApi.Domain.Enums;

namespace eMuhasebeApi.Domain.Entities;

public sealed class Invoice : Entity
{
    public DateOnly Date { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public InvoiceTypeEnum Type { get; set; } = InvoiceTypeEnum.Purchase;
    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public decimal Amount { get; set; }
    public List<InvoiceDetail>? Details { get; set; }
}