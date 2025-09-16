namespace InventorySystem.Application.Common.Interfaces.Services.Interfaces
{
    public interface IEmployeeContextService
    {
        Task<int> GetCurrentEmployeeIdAsync();
    }

}
