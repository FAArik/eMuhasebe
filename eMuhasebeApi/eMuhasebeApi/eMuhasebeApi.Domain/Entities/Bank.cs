﻿using eMuhasebeApi.Domain.Abstractions;
using eMuhasebeApi.Domain.Enums;

namespace eMuhasebeApi.Domain.Entities;

public sealed class Bank:Entity
{
    public string Name { get; set; }=string.Empty;
    public string IBAN { get; set; } = string.Empty;
    public CurrencyTypeEnum CurrencyType { get; set; } = CurrencyTypeEnum.TL;
    public decimal DepositAmount { get; set; }
    public decimal WithdrawalAmount { get; set; }
    public List<BankDetail>? Details { get; set; }
}
