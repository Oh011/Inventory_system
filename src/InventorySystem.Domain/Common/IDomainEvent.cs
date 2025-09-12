namespace Domain.Common
{
    // Domain/Common/IDomainEvent.cs
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }

}
