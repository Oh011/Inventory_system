using Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class ReadRepository<T, TKey> : RepositoryBase<T>, IReadRepository<T, TKey> where T : class
    {
        public ReadRepository(DbContext context) : base(context) { }



        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
       => await _context.Set<T>().AsNoTracking().AnyAsync(predicate);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true)
        {
            var query = _context.Set<T>().Where(predicate);
            return asNoTracking ? await query.AsNoTracking().ToListAsync() : await query.ToListAsync();
        }

        public IQueryable<T> GetAllAsIQueryableAsync(bool asNoTracking = true)
            => asNoTracking ? _context.Set<T>().AsNoTracking() : _context.Set<T>();

        public async Task<IEnumerable<T>> GetAllAsync(bool asNoTracking = true)
            => asNoTracking
                ? await _context.Set<T>().AsNoTracking().ToListAsync()
                : await _context.Set<T>().ToListAsync();

        public async Task<T?> GetById(TKey id)
            => await _context.Set<T>().FindAsync(id);

        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, bool asNoTracking = true)
        {
            var query = ApplySpecifications(specification);
            if (asNoTracking) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<TResult?> FirstOrDefaultAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector,
            bool asNoTracking = true)
        {
            var query = ApplySpecifications(specification);
            if (asNoTracking) query = query.AsNoTracking();
            return await query.Select(selector).FirstOrDefaultAsync();
        }

        public async Task<TResult?> FirstOrDefaultAsync<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            bool asNoTracking = true)
        {
            var query = _context.Set<T>().Where(predicate);
            if (asNoTracking) query = query.AsNoTracking();
            return await query.Select(selector).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithSpecifications(
            ISpecification<T> specification,
            bool asNoTracking = true)
        {
            var query = ApplySpecifications(specification);
            return asNoTracking ? await query.AsNoTracking().ToListAsync() : await query.ToListAsync();
        }

        public async Task<List<TResult>> ListAsync<TResult>(
            ISpecification<T> specifications,
            Expression<Func<T, TResult>> selector,
            bool asNoTracking = true)
        {
            var query = ApplySpecifications(specifications);
            if (asNoTracking) query = query.AsNoTracking();
            return await query.Select(selector).ToListAsync();
        }

        public async Task<List<TResult>> ListAsync<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            bool asNoTracking = true)
        {
            var query = _context.Set<T>().Where(predicate);
            if (asNoTracking) query = query.AsNoTracking();
            return await query.Select(selector).ToListAsync();
        }

        public async Task<List<TResult>> ListAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            bool asNoTracking = true)
        {
            var query = _context.Set<T>();


            return await query.Select(selector).ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
            => await _context.Set<T>().CountAsync(predicate);

        private IQueryable<T> ApplySpecifications(ISpecification<T> specification)
            => SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specification);


    }

}
