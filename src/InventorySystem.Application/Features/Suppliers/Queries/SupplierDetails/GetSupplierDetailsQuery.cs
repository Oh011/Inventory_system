using MediatR;
using Project.Application.Features.Suppliers.Dtos;

namespace Project.Application.Features.Suppliers.Queries.SupplierDetails
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
