namespace Project.Application.Common.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {


        public Task<int> Commit(CancellationToken cancellationToken = default);





        Task<ITransactionManager> BeginTransaction(CancellationToken cancellationToken = default);

        void ApplyRowVersion<T>(T entity, string rowVersion);


        void EnsureRowVersionMatch<T>(T entity, string rowVersion) where T : class;




        void DetachTrackedEntity<TEntity>(TEntity entity);


        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class;

    }
}
