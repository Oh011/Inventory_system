using Application.Exceptions;
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;

namespace Infrastructure.Services
{
    internal class AuthorizationService : IAuthorizationService
    {
        private readonly ICurrentUserService _currentUserService;

        public AuthorizationService(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public void AuthorizeEmployeeAccess(string employeeUserId)
        {
            if (!IsAdmin() && !IsSelf(employeeUserId))
                throw new ForbiddenException("You are not authorized to access this employee.");
        }

        public void EnsureSelf(string applicationUserId)
        {
            if (!IsSelf(applicationUserId))
                throw new ForbiddenException("You are only allowed to access your own profile.");
        }

        public bool IsAdmin()
            => _currentUserService.Role == "Admin";

        public bool IsSelf(string userId)
            => _currentUserService.UserId == userId;
    }
}
