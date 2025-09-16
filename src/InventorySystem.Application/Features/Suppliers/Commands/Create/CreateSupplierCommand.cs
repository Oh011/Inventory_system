using MediatR;
using InventorySystem.Application.Features.Suppliers.Dtos;

namespace InventorySystem.Application.Features.Suppliers.Commands.Create
{
    public class CreateSupplierCommand : SupplierCommandBase, IRequest<SupplierDto>
    {

    }
}
