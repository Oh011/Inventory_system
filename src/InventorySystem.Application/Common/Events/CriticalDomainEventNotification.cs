using Domain.Common;
using MediatR;

namespace InventorySystem.Application.Common.Events
{
    public class CriticalDomainEventNotification<TDomainEvent> : INotification
        where TDomainEvent : IDomainEvent
    {

        public TDomainEvent DomainEvent { get; }

        public CriticalDomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }

}
