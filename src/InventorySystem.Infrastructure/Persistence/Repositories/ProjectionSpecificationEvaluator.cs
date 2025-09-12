
using Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public static class ProjectionSpecificationEvaluator<T, TResult> where T : class
    {
        public static IQueryable<TResult> GetQuery(IQueryable<T> inputQuery, IProjectionSpecifications<T, TResult> spec)
        {
            var query = inputQuery;

            // Filtering
            if (spec is ISpecification<T> specification && specification.Criteria is not null)
                query = query.Where(specification.Criteria);



            if (spec is ISpecification<T> specifications && specifications.IgnoreQueryFilters == true)
                query = query.IgnoreQueryFilters();

            // Includes
            if (spec is IIncludeSpecification<T> includeSpec)
            {
                foreach (var include in includeSpec.IncludeExpressions)
                    query = query.Include(include);
            }

            // ThenIncludes
            if (spec is IThenIncludeSpecification<T> thenIncludeSpec)
            {
                foreach (var thenInclude in thenIncludeSpec.ThenIncludes)
                    query = thenInclude(query);
            }


            if (spec is IOrderingSpecification<T> orderingSpec)
            {
                if (orderingSpec.OrderBy is not null)
                    query = query.OrderBy(orderingSpec.OrderBy);
                else if (orderingSpec.OrderByDescending is not null)
                    query = query.OrderByDescending(orderingSpec.OrderByDescending);
            }

            // Projection
            var projectedQuery = query.Select(spec.Projection);

            // Result-level Ordering
            if (spec.ResultOrderBy is not null)
                projectedQuery = projectedQuery.OrderBy(spec.ResultOrderBy);
            else if (spec.ResultOrderByDescending is not null)
                projectedQuery = projectedQuery.OrderByDescending(spec.ResultOrderByDescending);

            // Pagination
            if (spec is IPaginationSpecification paginationSpec && paginationSpec.isPaginated)
            {
                projectedQuery = projectedQuery.Skip(paginationSpec.Skip).Take(paginationSpec.Take);
            }

            return projectedQuery;
        }
    }


}
