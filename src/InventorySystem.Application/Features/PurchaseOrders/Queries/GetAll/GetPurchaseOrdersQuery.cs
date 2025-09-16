using MediatR;
using InventorySystem.Application.Common.Enums.SortOptions;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;
using Shared.Parameters;
using Shared.Results;

namespace InventorySystem.Application.Features.PurchaseOrders.Queries.GetAll
{
    public class GetPurchaseOrdersQuery : PaginationQueryParameters, IRequest<PaginatedResult<PurchaseOrderListDto>>
    {

        public int? SupplierId { get; set; }
        public string? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public PurchaseOrderSortOptions? PurchaseOrderSortOptions { get; set; }
    }
}
