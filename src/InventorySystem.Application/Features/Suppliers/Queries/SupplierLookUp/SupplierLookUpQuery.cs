using MediatR;
using Project.Application.Features.Suppliers.Dtos;

namespace Project.Application.Features.Suppliers.Queries.SupplierLookUp
{
    public class SupplierLookUpQuery : IRequest<IEnumerable<SupplierLookupDto>>
    {
    }
}
