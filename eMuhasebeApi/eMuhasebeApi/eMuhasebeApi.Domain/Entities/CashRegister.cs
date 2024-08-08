using eMuhasebeApi.Domain.Abstractions;
using eMuhasebeApi.Domain.Enums;

namespace eMuhasebeApi.Domain.Entities;

public sealed class CashRegister : Entity
{
    public string Name { get; set; } = string.Empty;
    public CurrencyTypeEnum CurrencyType { get; set; } = CurrencyTypeEnum.TL;
    public decimal DepositAmount { get; set; }
    public decimal WithdrawalAmount { get; set; }
    public decimal BalanceAmount { get; set; }
    public  List<CashRegisterDetail>? Details { get; set; }
}
    