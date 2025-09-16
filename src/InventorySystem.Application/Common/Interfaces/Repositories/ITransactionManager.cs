namespace InventorySystem.Application.Common.Interfaces.Repositories
{
    public interface ITransactionManager
    {






        public Task CommitTransaction(CancellationToken cancellationToken = default);

        Task BeginTransactionAsync();

        public Task RollBackTransaction(CancellationToken cancellationToken = default);

    }
}
