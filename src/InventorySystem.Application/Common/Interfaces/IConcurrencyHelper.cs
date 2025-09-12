namespace Project.Application.Common.Interfaces
{
    public interface IConcurrencyHelper
    {


        Task ExecuteWithRetryAsync(Func<Task> action, int maxTries = 3);
    }
}
