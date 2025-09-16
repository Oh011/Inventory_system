using Microsoft.EntityFrameworkCore;
using InventorySystem.Application.Common.Interfaces.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class WriteRepository<T> : RepositoryBase<T>, IWriteRepository<T> where T : class
    {
        public WriteRepository(DbContext context) : base(context) { }

        public async Task AddAsync(T item) => await _dbSet.AddAsync(item);

        public void Update(T entity) => _dbSet.Update(entity);

        public void UpdateRange(IEnumerable<T> items) => _dbSet.UpdateRange(items);

        public void Delete(T item) => _dbSet.Remove(item);

        public async Task AddRangeAsync(IEnumerable<T> entites)
        {
            await _dbSet.AddRangeAsync(entites);
        }
    }

}
