using AutoMapper;
using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeApi.Application.Features.CashRegisters.GetAllCashRegisters;

internal sealed class GetAllCashRegistersQueryHandler(ICashRegisterRepository cashRegisterRepository,ICacheService cacheService) : IRequestHandler<GetAllCashRegistersQuery, Result<List<CashRegister>>>
{
    public async Task<Result<List<CashRegister>>> Handle(GetAllCashRegistersQuery request, CancellationToken cancellationToken)
    {
        List<CashRegister>? cashRegisters;
        cashRegisters =  cacheService.Get<List<CashRegister>>("cashRegisters");
        if (cashRegisters == null)
        {
            cashRegisters = await cashRegisterRepository
                .GetAll()
                .OrderBy(x => x.Name)
                .ToListAsync();
            cacheService.Set("cashRegisters", cashRegisters);
        }

        return cashRegisters;
    }
}
