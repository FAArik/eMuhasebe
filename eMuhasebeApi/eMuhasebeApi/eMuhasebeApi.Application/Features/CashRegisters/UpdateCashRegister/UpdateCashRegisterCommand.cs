using AutoMapper;
using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.CashRegisters.UpdateCashRegister;

public sealed record UpdateCashRegisterCommand(
    Guid Id,
    string Name,
    int TypeValue) : IRequest<Result<string>>;

internal sealed record UpdateCashRegisterCommandHandler(ICashRegisterRepository cashRegisterRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<UpdateCashRegisterCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateCashRegisterCommand request, CancellationToken cancellationToken)
    {
        CashRegister? cashRegister = await cashRegisterRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.Id);
        if (cashRegister is null)
        {
            return Result<string>.Failure("Kasa kaydı bulunamadı!");
        }
        if (cashRegister.Name != request.Name)
        {
            bool isNameExist = cashRegisterRepository.Any(x => x.Name == request.Name);
            return Result<string>.Failure("Bu kasa adı daha önceden kullanılmış");
        }
        mapper.Map(request, cashRegister);

        await unitOfWorkCompany.SaveChangesAsync();

        cacheService.Remove("cashRegisters");

        return "Kasa başarıyla güncellendi";

    }
}
