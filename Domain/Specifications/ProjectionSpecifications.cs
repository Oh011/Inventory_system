using System.Linq.Expressions;

namespace Domain.Specifications
{
    public class ProjectionSpecifications<T, TResult> : BaseSpecifications<T>, IProjectionSpecifications<T, TResult>
    {
        public Expression<Func<T, TResult>> Projection { get; private set; }

        public Expression<Func<TResult, object>>? ResultOrderBy { get; private set; }
        public Expression<Func<TResult, object>>? ResultOrderByDescending { get; private set; }



        protected ProjectionSpecifications(Expression<Func<T, bool>>? criteria = null) : base(criteria) { }

        public void AddProjection(Expression<Func<T, TResult>> projection)
        {

            this.Projection = projection;
        }

        public void SetResultOrderBy(Expression<Func<TResult, object>> expression)
        {
            this.ResultOrderBy = expression;
        }

        public void SetResultOrderByDescending(Expression<Func<TResult, object>> expression)
        {
            this.ResultOrderByDescending = expression;
        }
    }
}
