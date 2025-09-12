namespace Project.Application.Common.Interfaces.Services
{
    public interface IAuthorizationService
    {

        void AuthorizeEmployeeAccess(string employeeUserId);
        bool IsAdmin();

        void EnsureSelf(string applicationUserId);
        bool IsSelf(string userId);
    }
}
