using Domain.Common;
using MediatR;
using Project.Application.Common.Events;
using Project.Application.Common.Interfaces;

namespace Infrastructure.Events
{
    internal class DomainEventDispatcher : IDomainEventDispatcher
    {

        private readonly IMediator _mediator;


        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {


            foreach (var domainEvent in domainEvents)
            {

                var notification = CreateNotification(domainEvent);

                if (notification != null)
                {

                    await _mediator.Publish(notification, cancellationToken);
                }

            }
        }


        private static INotification? CreateNotification(IDomainEvent domainEvent)
        {



            var notificationType = typeof(DomainEventNotifications<>).MakeGenericType(domainEvent.GetType());
            return (INotification?)Activator.CreateInstance(notificationType, domainEvent);
        }
    }
}


//DomainEventNotifications<T> is a generic class.

//--> var notificationType = typeof(DomainEventNotifications<>).MakeGenericType(domainEvent.GetType());
//We are saying: “Create a type like DomainEventNotifications<PurchaseOrderCreatedDomainEvent>”.

//So notificationType now becomes:

//typeof(DomainEventNotifications<PurchaseOrderCreatedDomainEvent>)
