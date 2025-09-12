using Domain.Common;

namespace Project.Application.Common.Notifications.Senders
{

    // applying strategy pattern
    public interface INotificationSender<TEvent> where TEvent : IDomainEvent
    {
        Task SendAsync(TEvent domainEvent);
    }

}

