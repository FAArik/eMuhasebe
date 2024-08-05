using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.CashRegisterDetails.CreateCashRegisterDetail;

public sealed record CreateCashRegisterDetailCommand():IRequest<Result<string>>;


internal sealed class CreateCashRegisterDetailCommandHandler : IRequestHandler<CreateCashRegisterDetailCommand, Result<string>>
{
    public Task<Result<string>> Handle(CreateCashRegisterDetailCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
