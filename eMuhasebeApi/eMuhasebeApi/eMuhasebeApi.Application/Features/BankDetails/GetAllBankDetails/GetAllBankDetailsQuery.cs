using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeApi.Application.Features.BankDetails.GetAllBankDetails;

public sealed record GetAllBankDetailsQuery(
    Guid BankId,
    DateOnly StartDate,
    DateOnly EndDate
    ) : IRequest<Result<Bank>>;

internal sealed class GetAllBankDetailsQueryHandler(IBankRepository bankRepository) : IRequestHandler<GetAllBankDetailsQuery, Result<Bank>>
{
    public async Task<Result<Bank>> Handle(GetAllBankDetailsQuery request, CancellationToken cancellationToken)
    {
        Bank? bank = await bankRepository
            .Where(x => x.Id == request.BankId)
            .Include(x => x.Details!.Where(x => x.Date >= request.StartDate && x.Date <= request.EndDate))
            .FirstOrDefaultAsync(cancellationToken);

        if (bank is null)
        {
            return Result<Bank>.Failure("Banka bulunamadı");

        }
        return bank;
    }
}