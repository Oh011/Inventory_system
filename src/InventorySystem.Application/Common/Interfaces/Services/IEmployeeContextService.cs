namespace Project.Application.Common.Interfaces.Services
{
    public interface IEmployeeContextService
    {
        Task<int> GetCurrentEmployeeIdAsync();
    }

}
