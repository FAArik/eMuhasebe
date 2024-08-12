using AutoMapper;
using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Banks.CreateBank;

public sealed record CreateBankCommand(
    string Name,
    string IBAN,
    int CurrencyTypeValue
    ) : IRequest<Result<string>>;


internal sealed class CreateBankCommandHandler(
    IBankRepository bankRepository,
    IUnitOfWorkCompany unitOfWorkCompany,
    IMapper mapper,
    ICacheService cacheService
    ) : IRequestHandler<CreateBankCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateBankCommand request, CancellationToken cancellationToken)
    {
        bool isIBANExist = await bankRepository.AnyAsync(x => x.IBAN == request.IBAN, cancellationToken);

        if (isIBANExist)
        {
            return Result<string>.Failure("IBAN daha önce kaydedilmiş");
        }

        Bank bank = mapper.Map<Bank>(request);

        await bankRepository.AddAsync(bank, cancellationToken);
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("banks");
        return "Banka başarıyla kaydedildi";
    }
}