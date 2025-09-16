namespace InventorySystem.Application.Common.Interfaces.Services.Interfaces
{
    public interface IAuthorizationService
    {

        void AuthorizeEmployeeAccess(string employeeUserId);
        bool IsAdmin();

        void EnsureSelf(string applicationUserId);
        bool IsSelf(string userId);
    }
}
