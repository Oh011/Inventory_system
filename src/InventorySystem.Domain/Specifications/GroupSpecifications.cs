using System.Linq.Expressions;

namespace Domain.Specifications
{
    public abstract class GroupSpecification<T, TKey, TResult> : BaseSpecifications<T>,
        IGroupSpecifications<T, TKey, TResult>

    {
        protected GroupSpecification(Expression<Func<T, bool>>? Criteria) : base(Criteria)
        {
        }

        public Expression<Func<T, TKey>> GroupBy { get; private set; }
        public Expression<Func<IGrouping<TKey, T>, TResult>> GroupSelector { get; private set; }


        public Expression<Func<TResult, object>>? ResultOrderBy { get; private set; }
        public Expression<Func<TResult, object>>? ResultOrderByDescending { get; private set; }



        protected void AddGroupSelector(Expression<Func<IGrouping<TKey, T>, TResult>> Expression) => GroupSelector = Expression;

        protected void AddGroupBy(Expression<Func<T, TKey>> Expression) => GroupBy = Expression;


        protected void SetResultOrderBy(Expression<Func<TResult, object>> expression)
    => ResultOrderBy = expression;

        protected void SetResultOrderByDescending(Expression<Func<TResult, object>> expression)
            => ResultOrderByDescending = expression;
    }
}
