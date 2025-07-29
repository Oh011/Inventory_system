using Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    static class GroupSpecificationEvaluator<T, TKey, TResult> where T : class
    {



        public static IQueryable<TResult> GetQuery(IQueryable<T> inputQuery, IGroupSpecifications<T, TKey, TResult> specifications)
        {


            var query = inputQuery;



            //1- where


            if (specifications is ISpecification<T> specification && specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);

            }



            //2- Include

            if (specifications is IIncludeSpecification<T> includeSpec)
            {
                foreach (var include in includeSpec.IncludeExpressions)
                    query = query.Include(include);
            }


            //3-Group

            var groupedQuery = query
            .GroupBy(specifications.GroupBy)
            .Select(specifications.GroupSelector);


            // 4. Apply result ordering (if specified)

            if (specifications.ResultOrderBy != null)
                groupedQuery = groupedQuery.OrderBy(specifications.ResultOrderBy);
            else if (specifications.ResultOrderByDescending != null)
                groupedQuery = groupedQuery.OrderByDescending(specifications.ResultOrderByDescending);




            if (specifications is IPaginationSpecification paginationSpec && paginationSpec.isPaginated)
            {
                groupedQuery = groupedQuery.Skip(paginationSpec.Skip).Take(paginationSpec.Take);
            }

            return groupedQuery;

        }
    }
}
