using Application.Exceptions;
using Domain.Entities;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Features.Customers.Dtos;
using MediatR;

namespace InventorySystem.Application.Features.Customers.Commands.Update
{
    internal class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
    {

        private readonly IUnitOfWork unitOfWork;


        public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {

            var customerRepository = unitOfWork.GetRepository<Customer, int>();

            var customer = await customerRepository.GetById(request.Id);


            if (customer == null)
            {
                throw new NotFoundException($"Customer with id {request.Id} not found.");
            }

            if (request.FullName is not null)
                customer.FullName = request.FullName;

            if (request.Email is not null)
                customer.Email = request.Email;

            if (request.Phone is not null)
                customer.Phone = request.Phone;

            if (request.Address is not null)
                customer.Address = request.Address;


            customerRepository.Update(customer);

            await unitOfWork.Commit(cancellationToken);


            return new CustomerDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,

            };
        }
    }
}
