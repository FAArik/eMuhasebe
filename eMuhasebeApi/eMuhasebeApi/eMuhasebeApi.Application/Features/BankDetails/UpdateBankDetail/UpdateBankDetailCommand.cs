using eMuhasebeApi.Application.Features.BankDetails.UpdateBankDetail;
using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.BankDetails.UpdateBankDetail;

public sealed record UpdateBankDetailCommand(
    Guid Id,
    Guid BankId,
    int Type,
    DateOnly Date,
    decimal Amount,
    string Description
    ):IRequest<Result<string>>;

internal sealed class UodateBankDetailCommandHandler(
    IBankRepository  bankRepository,
    IBankDetailRepository bankDetailRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService
    ) : IRequestHandler<UpdateBankDetailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateBankDetailCommand request, CancellationToken cancellationToken)
    {
         BankDetail? bankDetail= await bankDetailRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.Id, cancellationToken);

        if (bankDetail is null)
        {
            return Result<string>.Failure("Banka hareketi bulunamadı");
        }
        Bank bank= await bankRepository.GetByExpressionWithTrackingAsync(x => x.Id == bankDetail.BankId, cancellationToken);

        if (bank is null)
        {
            return Result<string>.Failure("Banka bulunamadı");
        }
        bank.DepositAmount -= bankDetail.DepositAmount;
        bank.WithdrawalAmount -= bankDetail.WithdrawalAmount;

        bank.DepositAmount += request.Type == 0 ? request.Amount : 0;
        bank.WithdrawalAmount += request.Type == 1 ? request.Amount : 0;

        bankDetail.DepositAmount = request.Type == 0 ? request.Amount : 0;
        bankDetail.WithdrawalAmount = request.Type == 1 ? request.Amount : 0;

        bankDetail.Description = request.Description;
        bankDetail.Date = request.Date;

        await unitOfWorkCompany.SaveChangesAsync();

        cacheService.Remove("banks");

        return "Banka hareketi başarıyla güncellendi";
    }
}
