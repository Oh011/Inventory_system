using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;
using Project.Application.Common.Interfaces.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    internal class TransactionManager : ITransactionManager
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public TransactionManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction(CancellationToken cancellationToken = default)
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync(cancellationToken);
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollBackTransaction(CancellationToken cancellationToken = default)
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync(cancellationToken);
                await _transaction.DisposeAsync();
            }
        }
    }

}
