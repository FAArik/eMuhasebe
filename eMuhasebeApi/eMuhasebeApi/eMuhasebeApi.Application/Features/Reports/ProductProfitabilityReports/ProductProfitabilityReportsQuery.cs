using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Reports.ProductProfitabilityReports;

public sealed record ProductProfitabilityReportsQuery() : IRequest<Result<List<ProductProfitabilityReportsResponse>>>;

public sealed record ProductProfitabilityReportsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal DepositPrice { get; set; }
    public decimal WithdrawalPrice { get; set; }
    public decimal ProfitPercent { get; set; }
    public decimal ProfitAmount { get; set; }
};

internal sealed class ProductProfitabilityReportsQueryHandler(
    IProductRepository productRepository
) : IRequestHandler<ProductProfitabilityReportsQuery, Result<List<ProductProfitabilityReportsResponse>>>
{
    public async Task<Result<List<ProductProfitabilityReportsResponse>>> Handle(
        ProductProfitabilityReportsQuery request,
        CancellationToken cancellationToken)
    {
        List<Product> products = await productRepository
            .Where(x => x.Withdrawal > 0)
            .Include(x => x.Details)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

        List<ProductProfitabilityReportsResponse> res = new();

        foreach (var product in products)
        {
            decimal depositCount = product.Details!.Count(x => x.Deposit > 0);
            decimal depositPriceSum = product.Details!.Where(x => x.Deposit > 0).Sum(x => x.Price);

            decimal withdrawalCount = product.Details!.Count(x => x.Withdrawal > 0);
            decimal withdrawalPriceSum = product.Details!.Where(x => x.Withdrawal > 0).Sum(x => x.Price);
            decimal depositPrice = 0;
            if (depositPriceSum > 0 | depositCount > 0)
            {
                depositPrice = depositPriceSum / depositCount;
            }
            decimal withdrawalPrice = withdrawalPriceSum / withdrawalCount;
            decimal profitPercentAmount = 0;
            if (depositPrice > 0)
            {
                profitPercentAmount = withdrawalPrice/depositPrice;
            }
            ProductProfitabilityReportsResponse data = new()
            {
                Id = product.Id,
                Name = product.Name,
                DepositPrice = depositPrice,
                WithdrawalPrice = withdrawalPrice
            };
            data.ProfitAmount = data.WithdrawalPrice - data.DepositPrice;
            data.ProfitPercent = ((profitPercentAmount) - 1) * 100;
            res.Add(data);
        }

        return res;
    }
}