namespace eMuhasebeApi.Domain.Dtos;

public sealed record InvoiceDetailDto
{
    public Guid ProductId { get; init; }
    public decimal Quantity { get; init; }
    public decimal Price { get; init; }
}