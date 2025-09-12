using MediatR;
using Project.Application.Features.SalesInvoice.Dtos;

namespace Project.Application.Features.SalesInvoice.Queries.GetById
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
