using Domain.Common;

namespace Domain.Events.Products
{
    public sealed class ProductStockDecreasedEvent : IDomainEvent
    {
        public List<int> AffectedProductIds { get; }

        public DateTime OccurredOn { get; } = DateTime.Now;

        public ProductStockDecreasedEvent(IEnumerable<int> affectedProductIds)
        {
            AffectedProductIds = affectedProductIds.ToList();
        }
    }

}
