using Microsoft.AspNetCore.Http;
using Project.Application.Common.Interfaces.Services;
using System.Security.Claims;

namespace Infrastructure.Services
{
    internal class CurrentUserService
    : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

        public string Role =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value ?? "";

        public string? Email =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
    }

}
