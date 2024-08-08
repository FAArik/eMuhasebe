using eMuhasebeApi.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.CashRegisterDetails.GetAllCashRegisterDetailsQuery;

public sealed record GetAllCashRegisterDetailsQuery(
    Guid CashregiserId,
    DateOnly StartDate,
    DateOnly EndDate
    ) : IRequest<Result<CashRegister>>;
