using eMuhasebeApi.Application.Features.CashRegisterDetails.DeleteCashRegisterDetailById;
using MediatR;
using System.Xml.Linq;
using TS.Result;

namespace eMuhasebeApi.Application.Features.CashRegisterDetails.DeleteCashRegisterDetailById;

public sealed record DeleteCashRegisterDetailByIdCommand(
    Guid Id
    ):IRequest<Result<string>>;