using MediatR;

namespace InventorySystem.Application.Features.Customers.Commands.Delete
{
    public class DeleteCustomerCommand : IRequest
    {

        public int Id { get; set; }

        public DeleteCustomerCommand(int id)
        {
            Id = id;
        }
    }
}
