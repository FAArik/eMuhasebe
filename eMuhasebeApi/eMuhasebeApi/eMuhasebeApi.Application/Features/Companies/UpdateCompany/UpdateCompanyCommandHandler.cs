using AutoMapper;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Companies.UpdateCompany;

internal sealed class UpdateCompanyCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateCompanyCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        Company company = await companyRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.Id);
        if (company is null)
        {
            return Result<string>.Failure("Şirket bulunamadı");
        }
        if (company.TaxNumber != request.TaxNumber)
        {
            bool isTaxNumberExist = await companyRepository.AnyAsync(x => x.TaxNumber == request.TaxNumber, cancellationToken);
            if (isTaxNumberExist)
            {
                return Result<string>.Failure("Bu vergi numarası ile kayıtlı bir şirket bulunmaktadır.");
            }
        }
        mapper.Map(request,company);
        await unitOfWork.SaveChangesAsync();
        return "Şirket bilgisi güncellendi";
    }
}