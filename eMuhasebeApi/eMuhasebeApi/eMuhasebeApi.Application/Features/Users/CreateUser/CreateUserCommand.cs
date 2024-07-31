using eMuhasebeApi.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Users.CreateUser;

public sealed record CreateUserCommand(string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password,
    List<Guid>? CompanyIds,
    bool isAdmin
    ) : IRequest<Result<string>>;
