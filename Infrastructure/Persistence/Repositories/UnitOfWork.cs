using Application.Exceptions;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces.Repositories;
using System.Collections.Concurrent;

namespace Infrastructure.Persistence.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {


        private ConcurrentDictionary<string, object> Repositories;
        private readonly ApplicationDbContext _context;



        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Repositories = new ConcurrentDictionary<string, object>();
        }



        public async Task<int> Commit(CancellationToken cancellationToken = default)
        {



            return await _context.SaveChangesAsync(cancellationToken);


        }




        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
        {
            return (IGenericRepository<TEntity, TKey>)Repositories.GetOrAdd(typeof(TEntity).Name, (string key) => new GenericRepository<TEntity, TKey>(_context));
        }


        public void Dispose()
        {

            _context.Dispose();
        }


        public void EnsureRowVersionMatch<T>(T entity, string rowVersion) where T : class
        {
            var currentVersion = Convert.ToBase64String(_context.Entry(entity).Property("RowVersion").CurrentValue as byte[] ?? Array.Empty<byte>());

            if (currentVersion != rowVersion)
                throw new ConflictException("This resource was modified by another user. Please refresh and try again.");
        }


        public void DetachTrackedEntity<TEntity>(TEntity entity)
        {


            _context.Entry(entity).State = EntityState.Detached;
        }

        public void ApplyRowVersion<T>(T entity, string rowVersion)
        {
            _context.Entry(entity).OriginalValues["RowVersion"] = Convert.FromBase64String(rowVersion);
        }

        public async Task<ITransactionManager> BeginTransaction(CancellationToken cancellationToken = default)
        {
            var txManager = new TransactionManager(_context);
            await txManager.BeginTransactionAsync();
            return txManager;
        }

    }
}
