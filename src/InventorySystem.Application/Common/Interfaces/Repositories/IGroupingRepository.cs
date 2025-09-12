using Domain.Specifications;

namespace Project.Application.Common.Interfaces.Repositories
{
    public interface IGroupingRepository<T> where T : class
    {
        Task<IEnumerable<TResult>> GetAllWithGrouping<TResult, TKey>(
            GroupSpecification<T, TKey, TResult> specification);
    }

}
