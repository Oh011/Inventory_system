using Domain.Entities;
using Domain.Specifications;
using Project.Application.Features.PurchaseOrders.Dtos;

namespace Project.Application.Features.PurchaseOrders.Specifications
{
    public class PurchaseOrderDetailsSpecifications : ProjectionSpecifications<PurchaseOrder, PurchaseOrderResultDto>
    {




        public PurchaseOrderDetailsSpecifications(int id) : base(o => o.Id == id)
        {




            AddProjection(p => new PurchaseOrderResultDto
            {
                Id = p.Id,
                SupplierName = p.Supplier.Name,
                OrderDate = p.OrderDate,
                ExpectedDate = p.ExpectedDate,
                Status = p.Status.ToString(),
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
