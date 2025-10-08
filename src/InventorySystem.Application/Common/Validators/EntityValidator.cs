using Application.Exceptions;
using Domain.Common;
using InventorySystem.Application.Common.Interfaces.Repositories;
using Shared.Errors;
using Shared.Results;

namespace InventorySystem.Application.Common.Validators
{
    internal class EntityValidator<TEntity> : IEntityValidator<TEntity> where TEntity : BaseEntity
    {


        private readonly IUnitOfWork _unitOfWork;


        public EntityValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task ValidateExistAsync(IEnumerable<int> ids, string parameter)
        {


            var repo = _unitOfWork.GetRepository<TEntity, int>();

            var existingIds = (await repo.FindAsync(e => ids.Contains(e.Id)))
                              .Select(e => e.Id)
                              .ToList();

            var missingIds = ids.Except(existingIds).ToList();




            if (missingIds.Any())
            {


                var errors = missingIds.ToDictionary(
                    id => id.ToString(),
                    id => new List<ValidationErrorDetail>
                    {
                        new ValidationErrorDetail($"{parameter} with ID {id} not found")
                    }
                     );

                throw new ValidationException(errors, $"Some {parameter} are not found");


            }


        }



        public async Task ValidateExistsAsync(int id, string parameter)
        {
            var repo = _unitOfWork.GetRepository<TEntity, int>();
            var exists = await repo.ExistsAsync(e => e.Id == id);


            if (!exists)
            {
                throw new NotFoundException($"{parameter} with ID {id} was not found");
            }
        }


        public async Task<Result<Unit>> CheckExistAsync(IEnumerable<int> ids, string parameter)
        {
            var repo = _unitOfWork.GetRepository<TEntity, int>();

            var existingIds = (await repo.FindAsync(e => ids.Contains(e.Id)))
                              .Select(e => e.Id)
                              .ToList();

            var missingIds = ids.Except(existingIds).ToList();

            if (missingIds.Any())
            {
                var errors = missingIds.ToDictionary(
                    id => id.ToString(),
                    id => new List<ValidationErrorDetail>
                    {
                    new ValidationErrorDetail($"{parameter} with ID {id} not found")
                    });

                return Result<Unit>.Failure("Validation Errors", errors);
            }

            return Result<Unit>.Success();
        }

        public async Task<Result<Unit>> CheckExistsAsync(int id, string parameter)
        {
            var repo = _unitOfWork.GetRepository<TEntity, int>();
            var exists = await repo.ExistsAsync(e => e.Id == id);

            if (!exists)
            {
                return Result<Unit>.Failure(($"{parameter} with ID {id} was not found"), ErrorType.NotFound);
            }

            return Result<Unit>.Success();
        }
    }
}
