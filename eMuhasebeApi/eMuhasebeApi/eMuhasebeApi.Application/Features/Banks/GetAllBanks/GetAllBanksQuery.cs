using eMuhasebeApi.Domain.Entities;
using MediatR;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Banks.GetAllBanks;

public sealed record GetAllBanksQuery():IRequest<Result<List<Bank>>>;