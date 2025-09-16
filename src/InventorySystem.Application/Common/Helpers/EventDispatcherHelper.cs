using Domain.Common;
using InventorySystem.Application.Common.Interfaces;

namespace InventorySystem.Application.Common.Helpers
{
    public static class EventDispatcherHelper
    {

        public static async Task RaiseAndDispatch<T>(
            T entity,
            IDomainEventDispatcher dispatcher,
            Action<T> raiseEventAction,
            CancellationToken cancellationToken = default
        ) where T : BaseEntity
        {
            raiseEventAction(entity); // e.g., entity.MarkAsCreated();

            var events = entity.DomainEvents.ToList(); // Copy to avoid mutation issues

            await dispatcher.DispatchAsync(events, cancellationToken);

            entity.ClearDomainEvents();
        }


        public static async Task DispatchOnly(
    IDomainEvent domainEvent,
    IDomainEventDispatcher dispatcher,
    CancellationToken cancellationToken = default
)
        {
            var events = new List<IDomainEvent> { domainEvent };
            await dispatcher.DispatchAsync(events, cancellationToken);
        }

    }

}
