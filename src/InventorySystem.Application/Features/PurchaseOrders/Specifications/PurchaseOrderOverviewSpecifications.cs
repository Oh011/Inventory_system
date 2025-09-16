using Domain.Entities;
using Domain.Enums;
using Domain.Specifications;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;
using InventorySystem.Application.Features.PurchaseOrders.Queries.PurchaseOrderOverview;

namespace InventorySystem.Application.Features.PurchaseOrders.Specifications
{
    internal class PurchaseOrderOverviewSpecifications : GroupSpecification<PurchaseOrder, int, PurchaseOrderOverviewDto>
    {


        public PurchaseOrderOverviewSpecifications(GetPurchaseOrderOverviewQuery query, PurchaseOrderStatus? status) :
            base(
                  p =>
                (!query.SupplierId.HasValue || p.SupplierId == query.SupplierId.Value) &&

                (!query.FromDate.HasValue || p.OrderDate >= query.FromDate.Value) &&
                (!query.ToDate.HasValue || p.OrderDate <= query.ToDate.Value) &&

                (!status.HasValue || p.Status == status)

                )
        {


            AddGroupBy(p => 1);

            AddGroupSelector(g => new PurchaseOrderOverviewDto
            {
                PendingCount = g.Count(p => p.Status == PurchaseOrderStatus.Pending),
                ReceivedCount = g.Count(p => p.Status == PurchaseOrderStatus.Received),
                PartiallyReceivedCount = g.Count(p => p.Status == PurchaseOrderStatus.PartiallyReceived),
                CancelledCount = g.Count(p => p.Status == PurchaseOrderStatus.Cancelled),
                TotalPurchaseOrderValue = g.Sum(p => p.TotalAmount + p.DeliveryFee)
            });

        }
    }
}


//Count(p => ...) and Sum(p => ...) → translate into conditional aggregates in SQL
//(COUNT(CASE WHEN ...) and SUM(...))