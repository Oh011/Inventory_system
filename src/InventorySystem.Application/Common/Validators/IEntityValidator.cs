using Domain.Common;

namespace InventorySystem.Application.Common.Validators
{
    public interface IEntityValidator<TEntity> where TEntity : BaseEntity
    {

        Task ValidateExistsAsync(int id, string parameter);
        Task ValidateExistAsync(IEnumerable<int> ids, string parameter);
    }
}
