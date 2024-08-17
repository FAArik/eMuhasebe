using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.CashRegisterDetails.DeleteCashRegisterDetailById;

public sealed record DeleteCashRegisterDetailByIdCommand(
    Guid Id
    ):IRequest<Result<string>>;