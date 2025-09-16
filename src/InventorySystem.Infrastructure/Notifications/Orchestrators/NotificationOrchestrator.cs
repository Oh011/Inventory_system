using Domain.Common;
using InventorySystem.Application.Common.Notifications.Interfaces;
using InventorySystem.Application.Common.Notifications.Senders;

namespace Infrastructure.Notifications.Orchestrators
{
    public class NotificationOrchestrator<T> : INotificationOrchestrator<T> where T : IDomainEvent
    {
        private readonly INotificationSender<T> _sender;


        public NotificationOrchestrator(
           INotificationSender<T> sender)
        {
            _sender = sender;
        }


        public Task Notify(T domainEvent) => _sender.SendAsync(domainEvent);


    }

}
