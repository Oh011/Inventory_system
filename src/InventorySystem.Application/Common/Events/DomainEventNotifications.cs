using Domain.Common;
using MediatR;

namespace Project.Application.Common.Events
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
