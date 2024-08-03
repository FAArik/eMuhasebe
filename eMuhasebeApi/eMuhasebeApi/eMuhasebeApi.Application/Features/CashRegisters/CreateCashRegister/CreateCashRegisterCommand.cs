using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.CashRegisters.CreateCashRegister;

public sealed record CreateCashRegisterCommand(string Name, int TypeValue) : IRequest<Result<string>>;


