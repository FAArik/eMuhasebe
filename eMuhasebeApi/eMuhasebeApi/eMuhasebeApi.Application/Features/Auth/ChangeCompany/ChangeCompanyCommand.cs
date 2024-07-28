using eMuhasebeApi.Application.Features.Auth.Login;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Auth.ChangeCompany;

public sealed record ChangeCompanyCommand(
    Guid CompanyId
    ) : IRequest<Result<LoginCommandResponse>>;
