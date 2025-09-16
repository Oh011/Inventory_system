using MediatR;
using InventorySystem.Application.Features.SalesInvoice.Dtos;
using InventorySystem.Application.Features.SalesInvoice.Interfaces;
using Shared.Results;

namespace InventorySystem.Application.Features.SalesInvoice.Queries.GetAll
{
    public class GetSalesInvoicesQueryHandler : IRequestHandler<GetSalesInvoicesQuery, PaginatedResult<SalesInvoiceSummaryDto>>
    {


        private readonly ISalesInvoiceService salesInvoiceService;


        public GetSalesInvoicesQueryHandler(ISalesInvoiceService salesInvoiceService)
        {
            this.salesInvoiceService = salesInvoiceService;
        }

        public async Task<PaginatedResult<SalesInvoiceSummaryDto>> Handle(GetSalesInvoicesQuery request, CancellationToken cancellationToken)
        {

            var result = await salesInvoiceService.GetAllInvoices(request);


            return result;
        }
    }
}
