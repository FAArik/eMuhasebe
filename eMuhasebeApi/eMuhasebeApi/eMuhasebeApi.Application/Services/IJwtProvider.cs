using eMuhasebeApi.Application.Features.Auth.Login;
using eMuhasebeApi.Domain.Entities;

namespace eMuhasebeApi.Application.Services
{
    public interface IJwtProvider
    {
        Task<LoginCommandResponse> CreateToken(AppUser user);
    }
}
