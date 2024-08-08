using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.CashRegisterDetails.CreateCashRegisterDetail;

public sealed record CreateCashRegisterDetailCommand(
    Guid CashRegisterId,
    DateOnly Date,
    int Type,
    decimal Amount,
    Guid? CashRegisterDetailId,
    decimal OppositeAmount,
    string Description
    ) : IRequest<Result<string>>;
