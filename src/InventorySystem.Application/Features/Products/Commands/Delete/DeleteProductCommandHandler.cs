using Application.Exceptions;
using Domain.Entities;
using MediatR;
using InventorySystem.Application.Common.Interfaces.Repositories;

namespace InventorySystem.Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)

        {

            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Product, int>();
            var product = await repository.GetById(request.Id);

            if (product is null)
                throw new NotFoundException($"product with Id :{request.Id} is not found");



            product.IsDeleted = true;
            repository.Update(product);

            await _unitOfWork.Commit(cancellationToken);

            return Unit.Value;
        }
    }

}
