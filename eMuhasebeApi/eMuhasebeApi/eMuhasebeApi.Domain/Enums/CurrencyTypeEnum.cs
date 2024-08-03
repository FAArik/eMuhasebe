using Ardalis.SmartEnum;

namespace eMuhasebeApi.Domain.Enums;

public sealed class CurrencyTypeEnum : SmartEnum<CurrencyTypeEnum>
{
    
    public static readonly CurrencyTypeEnum TL = new("TL", 1);
    public static readonly CurrencyTypeEnum USD = new("USD", 2);
    public static readonly CurrencyTypeEnum EURO = new("EURO", 3);
    public CurrencyTypeEnum(string name, int value) : base(name, value)
    {
    }
}
