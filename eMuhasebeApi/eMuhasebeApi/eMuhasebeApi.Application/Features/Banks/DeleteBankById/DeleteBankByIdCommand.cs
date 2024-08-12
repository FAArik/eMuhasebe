using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Banks.DeleteBankById;

public sealed record DeleteBankByIdCommand(
   Guid Id
    ) : IRequest<Result<string>>;


internal sealed class DeleteBankByIdCommandHandler(
     IBankRepository bankRepository,
    ICacheService cacheService
    ) : IRequestHandler<string>
{
    public Task Handle(string request, CancellationToken cancellationToken)
    {
        return "";
    }
}