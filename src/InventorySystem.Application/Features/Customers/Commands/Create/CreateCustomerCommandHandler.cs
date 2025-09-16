using Application.Exceptions;
using Domain.Entities;
using MediatR;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Features.Customers.Dtos;

namespace InventorySystem.Application.Features.Customers.Commands.Create
{
    internal class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
    {


        private readonly IUnitOfWork _unitOfWork;


        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {


            var customerRepo = _unitOfWork.GetRepository<Customer, int>();

            // Check for existing phone
            if (!string.IsNullOrWhiteSpace(request.Phone))
            {
                var existingWithPhone = await customerRepo.ExistsAsync(
                    c => c.Phone == request.Phone);

                if (existingWithPhone)
                    throw new ConflictException($"A customer with phone '{request.Phone}' already exists.");
            }

            // Check for existing email
            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                var existingWithEmail = await customerRepo.ExistsAsync(
                    c => c.Email == request.Email);

                if (existingWithEmail)
                    throw new ConflictException($"A customer with email '{request.Email}' already exists.");
            }


            var customer = new Customer
            {
                FullName = request.FullName.Trim(),
                Phone = request.Phone?.Trim(),
                Email = request.Email?.Trim().ToLower(),
                Address = request.Address?.Trim()
            };

            await customerRepo.AddAsync(customer);
            await _unitOfWork.Commit(cancellationToken);

            return new CustomerDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address
            };

        }
    }
}
