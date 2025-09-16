namespace InventorySystem.Application.Common.Interfaces.Services.Interfaces
{
    public interface IStockEventService
    {
        Task RaiseStockDecreasedEventAsync(IEnumerable<int> productIds);
    }

}
