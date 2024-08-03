using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.CashRegisters.DeleteCashRegisterById;

public sealed record DeleteCashRequestByIdCommand(Guid Id) : IRequest<Result<string>>;

internal sealed class DeleteCashRequestByIdCommandHandler(ICashRegisterRepository cashRegisterRepository,IUnitOfWorkCompany unitOfWorkCompany,ICacheService cacheService): IRequestHandler<DeleteCashRequestByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteCashRequestByIdCommand request, CancellationToken cancellationToken)
    {
        CashRegister? cashRegister = await cashRegisterRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.Id);
        if (cashRegister is null)
        {
            return Result<string>.Failure("Kasa kaydı bulunamadı");
        }
        cashRegister.isDeleted = true;

        await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

        cacheService.Remove("cashRegisters");

        return "Kasa kaydı başarıyla silindi";
    }
}
