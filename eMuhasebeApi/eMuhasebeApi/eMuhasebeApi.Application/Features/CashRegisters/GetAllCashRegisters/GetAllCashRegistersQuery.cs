using eMuhasebeApi.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.CashRegisters.GetAllCashRegisters;

public sealed record GetAllCashRegistersQuery():IRequest<Result<List<CashRegister>>>;
