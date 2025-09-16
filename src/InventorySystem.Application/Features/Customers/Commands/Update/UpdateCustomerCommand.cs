using InventorySystem.Application.Features.Customers.Dtos;
using MediatR;

namespace InventorySystem.Application.Features.Customers.Commands.Update
{
    public class UpdateCustomerCommand : IRequest<CustomerDto>
    {


        public int Id { get; set; }
        public string FullName { get; set; } = null!;

        public string Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public UpdateCustomerCommand(int id, string fullName, string phone, string? email, string? address)
        {
            Id = id;
            FullName = fullName;
            Phone = phone;
            Email = email;
            Address = address;
        }
    }
}
