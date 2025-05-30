﻿using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eMuhasebeApi.Application.Features.Users.GetAllUsers;

internal sealed class GetAllUsersQueryHandler(
    UserManager<AppUser> userManager,
    ICacheService cacheService
    ) : IRequestHandler<GetAllUsersQuery, Result<List<AppUser>>>
{
    public async Task<Result<List<AppUser>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        List<AppUser>? users;

        users = cacheService.Get<List<AppUser>>("users");

        if (users is null)
        {
            users = await userManager.Users
                .Include(x => x.CompanyUsers!)
                .ThenInclude(x => x.Company)
                .OrderBy(x => x.FirstName)
                .ToListAsync(cancellationToken);

            cacheService.Set("users", users);
        }


        return users;
    }
}
