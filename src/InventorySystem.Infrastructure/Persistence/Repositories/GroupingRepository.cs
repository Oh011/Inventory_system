using Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using InventorySystem.Application.Common.Interfaces.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    internal class GroupingRepository<T> : RepositoryBase<T>, IGroupingRepository<T> where T : class
    {

        public GroupingRepository(DbContext context) : base(context) { }
        public async Task<IEnumerable<TResult>> GetAllWithGrouping<TResult, TKey>(
     GroupSpecification<T, TKey, TResult> specification)
        {
            var query = ApplyGroupingSpecifications(specification);
            return await query.ToListAsync();
        }

        private IQueryable<TResult> ApplyGroupingSpecifications<TResult, TKey>(
            IGroupSpecifications<T, TKey, TResult> specification)
        {
            return GroupSpecificationEvaluator<T, TKey, TResult>.GetQuery(_context.Set<T>(), specification);
        }
    }
}
