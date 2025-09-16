using Domain.Common;
using MediatR;

namespace InventorySystem.Application.Common.Events
{
    public class DomainEventNotifications<TDomainEvent> : INotification
        where TDomainEvent : IDomainEvent
    {

        public TDomainEvent DomainEvent { get; }

        public DomainEventNotifications(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }
}
