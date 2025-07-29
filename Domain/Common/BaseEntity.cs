namespace Domain.Common
{
    public class BaseEntity
    {


        public int Id { get; set; }

        private readonly List<IDomainEvent> _domainEvents = new();


        public bool IsDeleted { get; set; } = false;
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
