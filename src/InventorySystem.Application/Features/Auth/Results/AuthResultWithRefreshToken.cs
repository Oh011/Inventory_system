using Application.Features.Auth.Dtos;

namespace Application.Features.Auth.Results
{
    public class AuthResultWithRefreshToken
    {

        public LogInUserResponseDto Response { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
