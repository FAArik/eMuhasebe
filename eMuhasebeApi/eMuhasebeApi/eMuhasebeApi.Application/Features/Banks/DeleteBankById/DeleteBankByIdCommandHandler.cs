using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Banks.DeleteBankById;

internal sealed class DeleteBankByIdCommandHandler(
     IBankRepository bankRepository,
    ICacheService cacheService,
    IUnitOfWorkCompany unitOfWorkCompany
    ) : IRequestHandler<DeleteBankByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteBankByIdCommand request, CancellationToken cancellationToken)
    {
        Bank? bank = await bankRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.Id);
        if (bank is null)
        {
            return Result<string>.Failure("Banka bulunamadı");
        }
        bank.isDeleted = true;
        await unitOfWorkCompany.SaveChangesAsync();
        cacheService.Remove("banks");
        return "Banka başarıyla silindi";
    }
}