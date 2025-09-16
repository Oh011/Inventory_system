using Application.Exceptions;
using Domain.Entities;
using InventorySystem.Application.Common.Interfaces.Repositories;
using MediatR;

namespace InventorySystem.Application.Features.Customers.Commands.Delete
{
    internal class DeleteCustomerCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCustomerCommand>
    {


        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {

            var customerRepository = unitOfWork.GetRepository<Customer, int>();

            var customer = await customerRepository.GetById(request.Id);


            if (customer == null)
            {
                throw new NotFoundException($"Customer with id {request.Id} not found.");
            }


            customer.IsDeleted = true;

            customerRepository.Update(customer);

            await unitOfWork.Commit(cancellationToken);
        }
    }
}
