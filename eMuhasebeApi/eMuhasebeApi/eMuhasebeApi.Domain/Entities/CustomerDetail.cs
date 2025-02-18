using eMuhasebeApi.Domain.Abstractions;
using eMuhasebeApi.Domain.Enums;

namespace eMuhasebeApi.Domain.Entities;

public sealed class CustomerDetail : Entity
{
    public Guid CustomerId { get; set; }
    public DateOnly Date { get; set; }
    public CustomerDetailTypeEnum Type { get; set; } = CustomerDetailTypeEnum.CashRegister;
    public string Description { get; set; } = string.Empty;
    public Decimal DepositAmount { get; set; }
    public Decimal WithdrawalAmount { get; set; }
}