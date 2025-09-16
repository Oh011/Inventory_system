using Domain.Specifications;
using System.Linq.Expressions;

namespace InventorySystem.Application.Common.Interfaces.Repositories
{
    public interface IReadRepository<T, TKey> where T : class
    {
        Task<T?> GetById(TKey id);

        Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, bool asNoTracking = false);

        Task<TResult?> FirstOrDefaultAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector,
            bool asNoTracking = true);

        Task<TResult?> FirstOrDefaultAsync<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            bool asNoTracking = true);

        Task<IEnumerable<T>> FindAsync(
            Expression<Func<T, bool>> predicate,
            bool asNoTracking = true);

        Task<IEnumerable<T>> GetAllAsync(bool asNoTracking = true);

        IQueryable<T> GetAllAsIQueryableAsync(bool asNoTracking = true);

        Task<IEnumerable<T>> GetAllWithSpecifications(
            ISpecification<T> specification,
            bool asNoTracking = true);

        Task<List<TResult>> ListAsync<TResult>(
            ISpecification<T> specifications,
            Expression<Func<T, TResult>> selector,
            bool asNoTracking = true);

        Task<List<TResult>> ListAsync<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            bool asNoTracking = true);

        Task<List<TResult>> ListAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            bool asNoTracking = true);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }

}
