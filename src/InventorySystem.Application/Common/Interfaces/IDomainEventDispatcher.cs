using Domain.Common;

namespace Project.Application.Common.Interfaces
{
    public interface IDomainEventDispatcher
    {

        Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
    }
}
