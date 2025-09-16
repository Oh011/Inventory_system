using Domain.Enums;
using MediatR;
using InventorySystem.Application.Common.Enums.SortOptions;
using InventorySystem.Application.Features.SalesInvoice.Dtos;
using Shared.Parameters;
using Shared.Results;

namespace InventorySystem.Application.Features.SalesInvoice.Queries.GetAll
{
    public class GetSalesInvoicesQuery : PaginationQueryParameters, IRequest<PaginatedResult<SalesInvoiceSummaryDto>>
    {


        public int? CustomerId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public string? Search { get; set; }



        public SalesInvoiceSortOptions? salesInvoiceSortOptions { get; set; }


    }
}
