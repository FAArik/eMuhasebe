using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Users.UpdateUser;

public sealed record UpdateUserCommand(Guid Id,
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string? Password,
    List<Guid> CompanyIds
    ) : IRequest<Result<string>>;
