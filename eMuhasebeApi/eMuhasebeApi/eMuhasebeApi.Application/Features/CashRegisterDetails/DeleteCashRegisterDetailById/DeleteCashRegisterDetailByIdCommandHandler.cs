using eMuhasebeApi.Application.Features.CashRegisterDetails.DeleteCashRegisterDetailById;
using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using TS.Result;

internal sealed class DeleteCashRegisterDetailByIdCommandHandler(
    ICashRegisterRepository cashRegisterRepository,
    ICashRegisterDetailRepository cashRegisterDetailRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService
    ) : IRequestHandler<DeleteCashRegisterDetailByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteCashRegisterDetailByIdCommand request, CancellationToken cancellationToken)
    {
        CashRegisterDetail? cashRegisterDetail = await cashRegisterDetailRepository.GetByExpressionWithTrackingAsync(x=>x.Id==request.Id,cancellationToken);

        if(cashRegisterDetail is null)
        {
            return Result<string>.Failure("Kasa hareketi bulunamadı");
        }
        CashRegister cashRegister = await cashRegisterRepository.GetByExpressionWithTrackingAsync(x=>x.Id== cashRegisterDetail.Id,cancellationToken);

        if (cashRegister is null)
        {
            return Result<string>.Failure("Kasa bulunamadı");
        }
        cashRegister.DepositAmount -= cashRegisterDetail.DepositAmount;
        cashRegister.WithdrawalAmount -= cashRegisterDetail.WithdrawalAmount;

        if (cashRegisterDetail.CashRegisterDetailId is not null)
        {
            CashRegisterDetail? oppositeCashRegisterDetail = await cashRegisterDetailRepository.GetByExpressionWithTrackingAsync(x => x.Id == cashRegisterDetail.CashRegisterDetailId, cancellationToken);

            if (cashRegisterDetail is null)
            {
                return Result<string>.Failure("Kasa hareketi bulunamadı");
            }
            CashRegister oppositecashRegister = await cashRegisterRepository.GetByExpressionWithTrackingAsync(x => x.Id == oppositeCashRegisterDetail.CashRegisterId, cancellationToken);

            if (cashRegister is null)
            {
                return Result<string>.Failure("Kasa bulunamadı");
            }
            cashRegister.DepositAmount -= cashRegisterDetail.DepositAmount;
            cashRegister.WithdrawalAmount -= cashRegisterDetail.WithdrawalAmount;
        }

        cashRegisterDetailRepository.Delete(cashRegisterDetail);
        await unitOfWorkCompany.SaveChangesAsync();

        cacheService.Remove("cashRegisters");

        return "Kasa hareketi başarıyla silindi";
    }
}
