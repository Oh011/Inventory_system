using Application.Features.Auth.Results;

namespace Application.Features.Auth.Interfaces
{
    public interface ITokenService
    {

        string GenerateAccessToken(string UserName, string email, string id, IList<string> roles);


        Task<string> CreateRefreshTokenForDevice(string userId, string DeviceId);

        Task<AuthResultWithRefreshToken> RefreshAccessTokenAsync(string refreshToken, string DeviceId);


        Task<string> RevokeRefreshTokenByToken(string token, string DeviceId);

    }
}
