using AutoMapper;
using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.CashRegisters.CreateCashRegister;

internal sealed class CreateCashRegisterCommandHandler(ICashRegisterRepository cashRegisterRepository, IUnitOfWorkCompany unitOfWorkCompany, IMapper mapper, ICacheService cacheService) : IRequestHandler<CreateCashRegisterCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateCashRegisterCommand request, CancellationToken cancellationToken)
    {
        bool isNameExists = await cashRegisterRepository.AnyAsync(x => x.Name == request.Name,cancellationToken);

        if (!isNameExists)
        {
            return Result<string>.Failure("Bu kasa adı daha önceden kullanılmış");
        }
        CashRegister cashRegister = mapper.Map<CashRegister>(request);
        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("cashRegisters");

        return "Kasa kaydı başarıyla tamamlandı";
    }
}


