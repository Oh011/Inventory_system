using InventorySystem.Application.Features.PurchaseOrders.Dtos;
using MediatR;

namespace InventorySystem.Application.Features.PurchaseOrders.Queries.PurchaseOrderOverview
{
    public class GetPurchaseOrderOverviewQuery : IRequest<PurchaseOrderOverviewDto>
    {


        public int? SupplierId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }


    }
}
