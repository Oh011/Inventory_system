using MediatR;
using InventorySystem.Application.Features.Suppliers.Dtos;

namespace InventorySystem.Application.Features.Suppliers.Commands.Update
{
    public class UpdateSupplierRequest : UpdateSupplierCommand, IRequest<SupplierDto>
    {

        public int Id { get; set; }
    }
}
