using MediatR;
using Project.Application.Common.Enums.SortOptions;
using Project.Application.Features.PurchaseOrders.Dtos;
using Shared.Parameters;
using Shared.Results;

namespace Project.Application.Features.PurchaseOrders.Queries.GetAll
{
    public class GetPurchaseOrdersQuery : PaginationQueryParameters, IRequest<PaginatedResult<PurchaseOrderSummaryDto>>
    {

        public int? SupplierId { get; set; }
        public string? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public PurchaseOrderSortOptions? PurchaseOrderSortOptions { get; set; }
    }
}
