using MediatR;
using InventorySystem.Application.Features.SalesInvoice.Dtos;
using InventorySystem.Application.Features.SalesInvoice.Interfaces;

namespace InventorySystem.Application.Features.SalesInvoice.Queries.GetById
{
    public class GetSalesInvoiceByIdQueryHandler : IRequestHandler<GetSalesInvoiceByIdQuery, SalesInvoiceDetailsDto>
    {


        private readonly ISalesInvoiceService _salesInvoiceService;


        public GetSalesInvoiceByIdQueryHandler(ISalesInvoiceService salesInvoiceService)
        {
            this._salesInvoiceService = salesInvoiceService;
        }
        public async Task<SalesInvoiceDetailsDto> Handle(GetSalesInvoiceByIdQuery request, CancellationToken cancellationToken)
        {

            var result = await _salesInvoiceService.GetInvoiceById(request.Id);


            return result;
        }
    }
}
