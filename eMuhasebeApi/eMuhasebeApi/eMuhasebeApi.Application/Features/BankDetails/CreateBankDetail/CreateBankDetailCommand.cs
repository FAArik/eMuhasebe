﻿using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.BankDetails.CreateBankDetail;

public sealed record CreateBankDetailCommand(
    Guid BankId,
    DateOnly Date,
    int Type,
    decimal Amount,
    Guid? OppositeBankId,
    Guid? OppositeCashRegisterId,
   decimal OppositeAmount,
   string Description
    ) : IRequest<Result<string>>;


internal sealed class CreateBankDetailCommandHandler(
    ICashRegisterRepository cashRegisterRepository,
    ICashRegisterDetailRepository cashRegisterDetailRepository,
    IBankRepository bankRepository,
    IBankDetailRepository bankDetailRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    ICacheService cacheService
    ) : IRequestHandler<CreateBankDetailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateBankDetailCommand request, CancellationToken cancellationToken)
    {
        Bank bank = await bankRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.BankId, cancellationToken);

        bank.DepositAmount += (request.Type == 0 ? request.Amount : 0);
        bank.WithdrawalAmount += (request.Type == 1 ? request.Amount : 0);

        BankDetail bankDetail = new()
        {
            Date = request.Date,
            DepositAmount = request.Type == 0 ? request.Amount : 0,
            WithdrawalAmount = request.Type == 1 ? request.Amount : 0,
            Description = request.Description,
            BankId = request.BankId
        };
        await bankDetailRepository.AddAsync(bankDetail, cancellationToken);
        if (request.OppositeBankId is not null)
        {
            Bank oppositeBank = await bankRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.OppositeBankId, cancellationToken);
            oppositeBank.DepositAmount += (request.Type == 1 ? request.OppositeAmount : 0);
            oppositeBank.WithdrawalAmount += (request.Type == 0 ? request.OppositeAmount : 0);
            BankDetail oppositeBankDetail = new()
            {
                Date = request.Date,
                DepositAmount = request.Type == 1 ? request.OppositeAmount : 0,
                WithdrawalAmount = request.Type == 0 ? request.OppositeAmount : 0,
                BankDetailId = bankDetail.Id,
                Description = request.Description,
                BankId = (Guid)request.OppositeBankId
            };
            bankDetail.BankDetailId = oppositeBankDetail.Id;
            await bankDetailRepository.AddAsync(oppositeBankDetail, cancellationToken);
        }

        if (request.OppositeCashRegisterId is not null)
        {
            CashRegister oppositeCashRegister = await cashRegisterRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.OppositeCashRegisterId, cancellationToken);
            oppositeCashRegister.DepositAmount += (request.Type == 1 ? request.OppositeAmount : 0);
            oppositeCashRegister.WithdrawalAmount += (request.Type == 0 ? request.OppositeAmount : 0);
            CashRegisterDetail oppositeCashRegisterDetail = new()
            {
                Date = request.Date,
                DepositAmount = request.Type == 1 ? request.OppositeAmount : 0,
                WithdrawalAmount = request.Type == 0 ? request.OppositeAmount : 0,
                CashRegisterDetailId = bankDetail.Id,
                Description = request.Description,
                CashRegisterId = (Guid)request.OppositeCashRegisterId
            };
            bankDetail.CashRegisterDetailId = oppositeCashRegisterDetail.Id;
            await cashRegisterDetailRepository.AddAsync(oppositeCashRegisterDetail, cancellationToken);
        }

        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("banks");
        cacheService.Remove("cashRegisters"); 

        return "Kasa hareketi başarıyla işlendi";
    }
}