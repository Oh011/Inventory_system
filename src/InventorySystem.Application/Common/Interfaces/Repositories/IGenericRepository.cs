namespace InventorySystem.Application.Common.Interfaces.Repositories
{
    public interface IGenericRepository<T, TKey> : IReadRepository<T, TKey>,
    IWriteRepository<T>,
    IProjectionRepository<T>,
    IGroupingRepository<T>
    where T : class
    {


    }


}
