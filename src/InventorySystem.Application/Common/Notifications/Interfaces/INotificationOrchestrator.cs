namespace InventorySystem.Application.Common.Notifications.Interfaces
{
    public interface INotificationOrchestrator<T>
    {



        public Task Notify(T domainEvent);

    }
}
