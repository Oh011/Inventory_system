using System.Linq.Expressions;

namespace Domain.Specifications
{
    public interface ISpecification<T>
    {


        public Expression<Func<T, bool>> Criteria { get; }

        public bool IgnoreQueryFilters { get; }
    }


    public interface IIncludeSpecification<T>
    {

        public List<Expression<Func<T, object>>> IncludeExpressions { get; }
    }



    public interface IGroupSpecifications<T, TKey, TResult>
    {


        Expression<Func<T, TKey>> GroupBy { get; }

        Expression<Func<IGrouping<TKey, T>, TResult>> GroupSelector { get; }


        Expression<Func<TResult, object>>? ResultOrderBy { get; }
        Expression<Func<TResult, object>>? ResultOrderByDescending { get; }
    }
    public interface IProjectionSpecifications<T, TResult>
    {


        public Expression<Func<T, TResult>> Projection { get; }

        public Expression<Func<TResult, object>>? ResultOrderBy { get; }
        public Expression<Func<TResult, object>>? ResultOrderByDescending { get; }


        void AddProjection(Expression<Func<T, TResult>> projection);


        void SetResultOrderBy(Expression<Func<TResult, object>> expression);


        void SetResultOrderByDescending(Expression<Func<TResult, object>> expression);

    }


    public interface IThenIncludeSpecification<T>
    {

        public List<Func<IQueryable<T>, IQueryable<T>>> ThenIncludes { get; }
    }


    public interface IOrderingSpecification<T>
    {

        public Expression<Func<T, object>>? OrderBy { get; set; }





        public Expression<Func<T, object>>? OrderByDescending { get; set; }
    }


    public interface IPaginationSpecification
    {

        public int Take { get; set; }

        public int Skip { get; set; }


        public bool isPaginated { get; set; }
    }


}
