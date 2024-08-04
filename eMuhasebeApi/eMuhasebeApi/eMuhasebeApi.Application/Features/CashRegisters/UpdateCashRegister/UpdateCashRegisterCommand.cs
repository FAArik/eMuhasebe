using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.CashRegisters.UpdateCashRegister;

public sealed record UpdateCashRegisterCommand(
    Guid Id,
    string Name,
    int CurrencyTypeValue) : IRequest<Result<string>>;
