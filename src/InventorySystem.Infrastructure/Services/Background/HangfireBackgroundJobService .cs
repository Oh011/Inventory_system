using Hangfire;
using Project.Application.Common.Interfaces.Background;
using System.Linq.Expressions;

namespace Infrastructure.Services.Background
{
    internal class HangfireBackgroundJobService : IBackgroundJobService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public HangfireBackgroundJobService(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        public void Enqueue<TService>(Expression<Func<TService, Task>> methodCall) where TService : class
        {
            _backgroundJobClient.Enqueue<TService>(methodCall);
        }
    }

}
