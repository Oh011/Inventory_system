using System.Linq.Expressions;

namespace Project.Application.Common.Interfaces.Background
{
    // Core.Application.Interfaces
    public interface IBackgroundJobService
    {
        void Enqueue<TService>(Expression<Func<TService, Task>> methodCall) where TService : class;
    }

}
