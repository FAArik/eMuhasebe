using AutoMapper;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Companies.CreateCompany;

internal sealed class CreateCompanyCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateCompanyCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        bool isTaxNumberExist = await companyRepository.AnyAsync(x => x.TaxNumber == request.TaxNumber, cancellationToken);
        if (isTaxNumberExist)
        {
            return Result<string>.Failure("Bu vergi numarası ile kayıtlı bir şirket bulunmaktadır.");
        }
        Company company = mapper.Map<Company>(request);
        await companyRepository.AddAsync(company);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Şirket başarıyla oluşturuldu.";
    }
}