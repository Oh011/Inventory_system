using Application.Exceptions;
using Domain.Entities;
using MediatR;
using Project.Application.Common.Interfaces.Repositories;

namespace Project.Application.Features.Categories.Commands.Delete
{
    internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {


        private readonly IUnitOfWork unitOfWork;


        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {

            var repository = unitOfWork.GetRepository<Category, int>();

            var category = await repository.GetById(request.Id);



            if (category == null)
                throw new NotFoundException("Category not found");

            category.IsDeleted = true;


            await unitOfWork.Commit();

            return Unit.Value; // Means: success but no result
        }
    }
}
