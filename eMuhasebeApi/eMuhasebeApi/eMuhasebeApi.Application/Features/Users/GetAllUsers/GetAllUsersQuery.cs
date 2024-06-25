using eMuhasebeApi.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Users.GetAllUsers;

public sealed record GetAllUsersQuery():IRequest<Result<List<AppUser>>>;
