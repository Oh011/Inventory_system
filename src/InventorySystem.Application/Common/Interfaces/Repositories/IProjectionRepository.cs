using Domain.Specifications;

namespace Project.Application.Common.Interfaces.Repositories
{
    public interface IProjectionRepository<T> where T : class
    {
        Task<TResult> GetByIdWithProjectionSpecifications<TResult>(
            IProjectionSpecifications<T, TResult> specifications,
            bool asNoTracking = true);

        Task<IEnumerable<TResult>> GetAllWithProjectionSpecifications<TResult>(
            IProjectionSpecifications<T, TResult> specifications,
            bool asNoTracking = true);
    }

}
