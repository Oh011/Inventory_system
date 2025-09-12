namespace Project.Application.Common.Interfaces.Repositories
{
    public interface IWriteRepository<T> where T : class
    {
        Task AddAsync(T item);


        Task AddRangeAsync(IEnumerable<T> entites);
        void Update(T entity);

        void UpdateRange(IEnumerable<T> items);

        void Delete(T item);
    }

}
