using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeApi.Application.Features.CashRegisterDetails.GetAllCashRegisterDetailsQuery;

internal sealed class GetAllCashRegisterDetailsQueryHandler(ICashRegisterRepository cashRegisterRepository) : IRequestHandler<GetAllCashRegisterDetailsQuery, Result<CashRegister>>
{
    public async Task<Result<CashRegister>> Handle(GetAllCashRegisterDetailsQuery request, CancellationToken cancellationToken)
    {
        CashRegister? cashRegister = await cashRegisterRepository
            .Where(x => x.Id == request.CashregiserId)
            .Include(x => x.Details!.Where(x => x.Date >= request.StartDate && x.Date <= request.EndDate)).FirstOrDefaultAsync(cancellationToken);


        if (cashRegister is null)
        {
            return Result<CashRegister>.Failure("Kasa bulunamadı");
        }
        return cashRegister;
    }
}
