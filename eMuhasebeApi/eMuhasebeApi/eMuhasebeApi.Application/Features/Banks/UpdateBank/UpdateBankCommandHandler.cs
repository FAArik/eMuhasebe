using AutoMapper;
using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Banks.UpdateBank;

internal sealed class UpdateBankCommandHandler(
    IBankRepository bankRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    IMapper mapper,
    ICacheService cacheService
    ) : IRequestHandler<UpdateBankCommand, Result<string>>
{
    
    public async Task<Result<string>> Handle(UpdateBankCommand request, CancellationToken cancellationToken)
    {
        Bank? bank = await bankRepository.GetByExpressionWithTrackingAsync(x=>x.Id==request.Id);

        if (bank is null)
        {
            return Result<string>.Failure("Banka bulunamadı");
        }

        if (bank.IBAN != request.IBAN)
        {
            bool isIBANExist = bankRepository.Any(x => x.IBAN == request.IBAN);
            if (isIBANExist)
            {
                return Result<string>.Failure("IBAN daha önce kaydedilmiş");
            }
        }

        mapper.Map(request, bank);

        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("banks");

        return "Banka bilgileri başarıyla güncellendi";
    }
}