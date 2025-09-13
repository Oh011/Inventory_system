
using Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public static class SpecificationEvaluator<T> where T : class
    {


        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery, ISpecification<T> specification)
        {



            var query = InputQuery;



            if (specification.IgnoreQueryFilters == true)
                query.IgnoreQueryFilters();


            if (specification.Criteria != null)
            {

                query = query.Where(specification.Criteria);
            }


            if (specification is IIncludeSpecification<T> includeSpecification)
            {


                foreach (var item in includeSpecification.IncludeExpressions)
                {

                    query = query.Include(item);

                }

            }




            if (specification is IOrderingSpecification<T> orderingSpec)
            {
                if (orderingSpec.OrderBy is not null)
                    query = query.OrderBy(orderingSpec.OrderBy);
                else if (orderingSpec.OrderByDescending is not null)
                    query = query.OrderByDescending(orderingSpec.OrderByDescending);
            }


            if (specification is IPaginationSpecification paginationSpec && paginationSpec.isPaginated)
            {
                query = query.Skip(paginationSpec.Skip).Take(paginationSpec.Take);
            }

            return query;
        }
    }
}
