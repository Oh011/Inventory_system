namespace Application.Features.Auth.Dtos
{
    public class LogInUserResponseDto
    {

        public string AccessToken { get; set; } = default!;

        public DateTime AccessTokenExpiresAt { get; set; }

        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}
