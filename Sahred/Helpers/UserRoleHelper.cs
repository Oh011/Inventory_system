
using Domain.Enums;

namespace Shared.Helpers
{
    public static class UserRoleHelper
    {
        public static string ToRoleName(this UserRole role)
        {
            return role switch
            {
                UserRole.Admin => "Admin",
                UserRole.Manager => "Manager",
                UserRole.Salesperson => "Salesperson",
                UserRole.Warehouse => "Warehouse",
                _ => throw new ArgumentOutOfRangeException(nameof(role), "Invalid user role")
            };
        }
    }

}
