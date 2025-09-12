using MediatR;
using Project.Application.Features.Customers.Dtos;

namespace Project.Application.Features.Customers.Commands.Create
{


    public class CreateCustomerCommand : IRequest<CustomerDto>
    {


        public string FullName { get; set; } = null!;

        public string Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }


    }
}
