using eMuhasebeApi.Domain.Abstractions;

namespace eMuhasebeApi.Domain.Entities;

public sealed class BankDetail : Entity
{
    public Guid BankId { get; set; }
    public DateOnly Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public Decimal DepositAmount { get; set; }
    public Decimal WithdrawalAmount { get; set; }
    public Guid? BankDetailId { get; set; }
    //public BankDetail? BankDetailOpposite { get; set; } 
    public Guid? CashRegisterDetailId { get; set; }

}
