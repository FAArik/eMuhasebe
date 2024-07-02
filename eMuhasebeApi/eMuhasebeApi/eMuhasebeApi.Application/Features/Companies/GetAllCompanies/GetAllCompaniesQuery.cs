using eMuhasebeApi.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Companies.GetAllCompanies;

public sealed record GetAllCompaniesQuery() : IRequest<Result<List<Company>>>;
