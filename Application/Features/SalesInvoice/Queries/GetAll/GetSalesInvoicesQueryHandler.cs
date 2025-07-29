using MediatR;
using Project.Application.Features.SalesInvoice.Dtos;
using Project.Application.Features.SalesInvoice.Interfaces;
using Shared.Results;

namespace Project.Application.Features.SalesInvoice.Queries.GetAll
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
