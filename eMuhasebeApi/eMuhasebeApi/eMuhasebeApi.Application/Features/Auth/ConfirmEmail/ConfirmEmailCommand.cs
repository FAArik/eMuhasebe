using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Auth.ConfirmEmail;

public sealed record ConfirmEmailCommand(string email) : IRequest<Result<string>>;