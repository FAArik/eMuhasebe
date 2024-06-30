using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Auth.SendConfirmEmail;

public sealed record SendConfirmEmailCommand(string email) : IRequest<Result<string>>;