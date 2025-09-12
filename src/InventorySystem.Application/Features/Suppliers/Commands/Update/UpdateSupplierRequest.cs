using MediatR;
using Project.Application.Features.Suppliers.Dtos;

namespace Project.Application.Features.Suppliers.Commands.Update
{
    public class UpdateSupplierRequest : UpdateSupplierCommand, IRequest<SupplierDto>
    {

        public int Id { get; set; }
    }
}
