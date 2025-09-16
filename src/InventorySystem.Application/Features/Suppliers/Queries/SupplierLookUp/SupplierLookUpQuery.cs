using MediatR;
using InventorySystem.Application.Features.Suppliers.Dtos;

namespace InventorySystem.Application.Features.Suppliers.Queries.SupplierLookUp
{
    public class SupplierLookUpQuery : IRequest<IEnumerable<SupplierLookupDto>>
    {
    }
}
