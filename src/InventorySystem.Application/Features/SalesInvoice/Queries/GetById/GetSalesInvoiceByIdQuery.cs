using MediatR;
using InventorySystem.Application.Features.SalesInvoice.Dtos;

namespace InventorySystem.Application.Features.SalesInvoice.Queries.GetById
{
    public class GetSalesInvoiceByIdQuery : IRequest<SalesInvoiceDetailsDto>
    {

        public int Id { get; set; }


        public GetSalesInvoiceByIdQuery(int id)
        {
            Id = id;
        }
    }
}
