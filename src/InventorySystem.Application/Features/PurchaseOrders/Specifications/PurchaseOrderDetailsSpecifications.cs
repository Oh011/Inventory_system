using Domain.Entities;
using Domain.Specifications;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;
using Shared.Extensions;

namespace InventorySystem.Application.Features.PurchaseOrders.Specifications
{
    public class PurchaseOrderDetailsSpecifications : ProjectionSpecifications<PurchaseOrder, PurchaseOrderDetailDto>
    {




        public PurchaseOrderDetailsSpecifications(int id) : base(o => o.Id == id)
        {




            AddProjection(p => new PurchaseOrderDetailDto
            {
                Id = p.Id,
                SupplierName = p.Supplier.Name,
                OrderDate = p.OrderDate,
                ExpectedDate = p.ExpectedDate,
                Status = p.Status.ToReadableString(),
                TotalAmount = p.TotalAmount,
                DeliveryFee = p.DeliveryFee,
                Notes = p.Notes,
                RowVersion = Convert.ToBase64String(p.RowVersion),
                OrderItems = p.Items.Select(i => new PurchaseOrderItemResultDto
                {
                    ProductId = i.ProductId,
                    ItemId = i.Id,
                    ProductName = i.Product.Name,
                    ProductCode = i.Product.Barcode,
                    UnitCost = i.UnitCost,
                    QuantityOrdered = i.QuantityOrdered,
                    QuantityReceived = i.QuantityReceived
                }).ToList()

            });



        }

    }
}
