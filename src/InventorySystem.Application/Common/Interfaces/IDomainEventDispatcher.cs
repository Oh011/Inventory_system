using Domain.Common;

namespace InventorySystem.Application.Common.Interfaces
{
    public interface IDomainEventDispatcher
    {

        Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
    }
}
