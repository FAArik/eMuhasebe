using eMuhasebeApi.WebAPI.ValueObject;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Companies.CreateCompany;

public sealed record CreateCompanyCommand(
    string Name,
    string FullAddress,
    string TaxNumber,
    string TaxDepartment,
    Database Database) : IRequest<Result<string>>;
