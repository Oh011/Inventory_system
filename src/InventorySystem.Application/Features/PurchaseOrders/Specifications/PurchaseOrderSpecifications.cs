using Domain.Entities;
using Domain.Specifications;
using InventorySystem.Application.Common.Enums.SortOptions;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;
using InventorySystem.Application.Features.PurchaseOrders.Queries.GetAll;
using Shared.Extensions;

namespace InventorySystem.Application.Features.PurchaseOrders.Specifications
{
    public class PurchaseOrderSpecifications : ProjectionSpecifications<PurchaseOrder, PurchaseOrderListDto>
    {



        public PurchaseOrderSpecifications(GetPurchaseOrdersQuery query) :
            base(

                p =>
                (!query.SupplierId.HasValue || p.SupplierId == query.SupplierId.Value) &&
                (string.IsNullOrEmpty(query.Status) || p.Status.ToString() == query.Status) &&
                (!query.StartDate.HasValue || p.OrderDate >= query.StartDate.Value) &&
                (!query.EndDate.HasValue || p.OrderDate <= query.EndDate.Value)


                )
        {


            //AddInclude(p => p.Supplier);

            AddProjection(p => new PurchaseOrderListDto
            {
                Id = p.Id,
                SupplierId = p.SupplierId,
                SupplierName = p.Supplier.Name,
                OrderDate = p.OrderDate,
                Status = p.Status.ToReadableString(),
                GrandTotal = p.TotalAmount + p.DeliveryFee
            });

            switch (query.PurchaseOrderSortOptions)
            {
                case PurchaseOrderSortOptions.OrderDateAsc:
                    SetResultOrderBy(p => p.OrderDate);
                    break;
                case PurchaseOrderSortOptions.OrderDateDesc:
                    SetResultOrderByDescending(p => p.OrderDate);
                    break;
                case PurchaseOrderSortOptions.GrandTotalAsc:
                    SetResultOrderBy(p => p.GrandTotal);
                    break;
                case PurchaseOrderSortOptions.GrandTotalDesc:
                    SetResultOrderByDescending(p => p.GrandTotal);
                    break;
                default:
                    SetResultOrderByDescending(p => p.OrderDate);
                    break;
            }

            ApplyPagination(query.PageIndex, query.pageSize);

        }
    }
}
