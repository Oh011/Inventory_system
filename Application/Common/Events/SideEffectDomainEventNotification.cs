using Domain.Common;
using MediatR;

namespace Project.Application.Common.Events
{
    internal class SideEffectDomainEventNotification<TDomainEvent> : INotification
        where TDomainEvent : IDomainEvent
    {

        public TDomainEvent DomainEvent { get; }

        public SideEffectDomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }


}
