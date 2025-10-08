using Domain.Common;
using Shared.Results;

namespace InventorySystem.Application.Common.Validators
{
    public interface IEntityValidator<TEntity> where TEntity : BaseEntity
    {

        Task ValidateExistsAsync(int id, string parameter);
        Task ValidateExistAsync(IEnumerable<int> ids, string parameter);



        public Task<Result<Unit>> CheckExistAsync(IEnumerable<int> ids, string parameter);


        public Task<Result<Unit>> CheckExistsAsync(int id, string parameter);
    }
}
