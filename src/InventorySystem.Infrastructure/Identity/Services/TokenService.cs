using Application.Exceptions;
using Application.Features.Auth.Dtos;
using Application.Features.Auth.Interfaces;
using Application.Features.Auth.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Project.Application.Common.Interfaces.Repositories;
using Sahred.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity.Services
{
    internal class TokenService(IOptions<JwtOptions> options, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : ITokenService
    {
        public string GenerateAccessToken(string UserName, string email, string id, IList<string> roles)
        {


            var jwtOptions = options.Value;

            var claims = new List<Claim>
            {


                new Claim(ClaimTypes.Name, UserName),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, id),

            };


            foreach (var role in roles)
            {

                claims.Add(new Claim(ClaimTypes.Role, role));
            }



            //3-key


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));


            var SignCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var tokenDescriptor = new SecurityTokenDescriptor()
            {

                Subject = new ClaimsIdentity(claims),
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audiance,
                Expires = DateTime.UtcNow.AddHours(jwtOptions.ExpirationInHours),

                SigningCredentials = SignCred



            };



            var tokenHandler = new JwtSecurityTokenHandler();
            var Token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(Token);

        }


        //----------------------------------------------------------------------------------------------

        public async Task<string> CreateRefreshTokenForDevice(string userId, string DeviceId)
        {


            var refreshTokenRepository = unitOfWork.GetRepository<RefreshToken, int>();

            var oldToken = (await refreshTokenRepository.
                FindAsync(t => t.UserId == userId && t.Revoked == false && t.DeviceId == DeviceId, true)

                ).FirstOrDefault();



            if (oldToken != null)
            {

                await RevokeRefreshTokenAsync(oldToken);
            }


            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = GenerateRefreshToken(),
                Expiration = DateTime.UtcNow.AddDays(14), // Typically 7 days expiration
                CreatedAt = DateTime.UtcNow,
                Revoked = false,
                DeviceId = DeviceId,

            };

            await refreshTokenRepository.AddAsync(refreshToken);

            await unitOfWork.Commit();


            return refreshToken.Token;


        }




        public async Task<string> RevokeRefreshTokenByToken(string token, string DeviceId)
        {



            var refreshToken = await GetValidRefreshTokenOrThrow(token, DeviceId);

            await RevokeRefreshTokenAsync(refreshToken);

            return "Logged out successfully.";

        }



        public async Task<AuthResultWithRefreshToken> RefreshAccessTokenAsync(string refreshToken, string DeviceId)
        {



            var storedToken = await GetValidRefreshTokenOrThrow(refreshToken, DeviceId);

            var user = await userManager.FindByIdAsync(storedToken.UserId);

            if (user == null)
                throw new NotFoundException("User not found.");

            var roles = await userManager.GetRolesAsync(user);

            var accessToken = GenerateAccessToken(user.UserName, user.Email, user.Id, roles);


            await this.RevokeRefreshTokenAsync(storedToken);


            var newRefreshToken = await CreateRefreshTokenForDevice(user.Id, DeviceId);



            return new AuthResultWithRefreshToken
            {
                Response = new LogInUserResponseDto
                {
                    AccessToken = accessToken,
                    AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(60),
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = roles.FirstOrDefault()

                },
                RefreshToken = refreshToken
            };


        }


        private async Task<RefreshToken> GetValidRefreshTokenOrThrow(string token, string deviceId)
        {
            var refreshTokenRepository = unitOfWork.GetRepository<RefreshToken, int>();

            var refreshToken = (await refreshTokenRepository
                .FindAsync(t => t.Token == token && t.DeviceId == deviceId))
                .FirstOrDefault();

            if (refreshToken == null || refreshToken.Expiration < DateTime.UtcNow)
                throw new UnAuthorizedException("Invalid or expired refresh token.");



            if (refreshToken.Revoked)
                throw new ForbiddenException("This refresh token has already been revoked.");

            return refreshToken;
        }



        private async Task RevokeRefreshTokenAsync(RefreshToken token)
        {

            var refreshTokenRepository = unitOfWork.GetRepository<RefreshToken, int>();
            token.Revoked = true;
            refreshTokenRepository.Update(token);
            await unitOfWork.Commit();


        }



        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32]; // 256-bit token
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }


    }
}


//0RoSySlrNM2sE1o+9xaxaGfC2ynNqakwNONkunAtykM= in databse
//0RoSySlrNM2sE1o%2B9xaxaGfC2ynNqakwNONkunAtykM%3D in cokkies