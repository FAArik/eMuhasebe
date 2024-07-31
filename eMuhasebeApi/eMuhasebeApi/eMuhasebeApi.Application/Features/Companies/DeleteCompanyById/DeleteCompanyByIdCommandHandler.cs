using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Companies.DeleteCompanyById;

internal sealed class DeleteCompanyByIdCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork, ICacheService cacheService) : IRequestHandler<DeleteCompanyByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteCompanyByIdCommand request, CancellationToken cancellationToken)
    {
        Company company = await companyRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.Id);
        if (company is null)
        {
            return Result<string>.Failure("Şirket bulunamadı");
        }
        company.isDeleted = true;
        await unitOfWork.SaveChangesAsync();

        cacheService.Remove("companies");

        return "Şirket başarıyla silindi";
    }
}