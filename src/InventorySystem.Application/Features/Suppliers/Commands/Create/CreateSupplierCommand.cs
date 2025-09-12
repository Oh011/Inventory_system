using MediatR;
using Project.Application.Features.Suppliers.Dtos;

namespace Project.Application.Features.Suppliers.Commands.Create
{
    public class CreateSupplierCommand : SupplierCommandBase, IRequest<SupplierDto>
    {

    }
}
