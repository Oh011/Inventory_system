using MediatR;
using InventorySystem.Application.Features.Suppliers.Dtos;

namespace InventorySystem.Application.Features.Suppliers.Queries.SupplierDetails
{
    public class GetSupplierDetailsQuery : IRequest<SupplierDetailDto>
    {

        public int Id { get; set; }


        public GetSupplierDetailsQuery(int id)
        {
            Id = id;
        }
    }
}
