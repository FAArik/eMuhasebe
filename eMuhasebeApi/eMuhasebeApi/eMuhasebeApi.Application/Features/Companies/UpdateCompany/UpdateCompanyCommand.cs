using eMuhasebeApi.WebAPI.ValueObject;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Companies.UpdateCompany;

public sealed record UpdateCompanyCommand(
    Guid Id,
    string Name,
    string FullAddress,
    string TaxNumber,
    string TaxDepartment,
    Database Database) : IRequest<Result<string>>;
