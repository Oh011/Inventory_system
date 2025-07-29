using Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class ProjectionRepository<T> : RepositoryBase<T>, IProjectionRepository<T> where T : class
    {
        public ProjectionRepository(DbContext context) : base(context) { }


        public async Task<TResult> GetByIdWithProjectionSpecifications<TResult>(
      IProjectionSpecifications<T, TResult> specifications,
      bool asNoTracking = true)
        {
            var query = ApplyProjectionSpecifications(specifications);

            ;
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TResult>> GetAllWithProjectionSpecifications<TResult>(
            IProjectionSpecifications<T, TResult> specifications,
            bool asNoTracking = true)
        {
            var query = ApplyProjectionSpecifications(specifications);


            return await query.ToListAsync();
        }

        private IQueryable<TResult> ApplyProjectionSpecifications<TResult>(IProjectionSpecifications<T, TResult> specifications)
            => ProjectionSpecificationEvaluator<T, TResult>.GetQuery(_context.Set<T>(), specifications);
    }

}
