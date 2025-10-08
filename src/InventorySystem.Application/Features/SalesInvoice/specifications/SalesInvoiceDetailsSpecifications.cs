using Domain.Specifications;
using InventorySystem.Application.Features.SalesInvoice.Dtos;

using salesInvoice = Domain.Entities.SalesInvoice;

namespace InventorySystem.Application.Features.SalesInvoice.specifications
{
    public class SalesInvoiceDetailsSpecifications : ProjectionSpecifications<salesInvoice, SalesInvoiceDetailsDto>
    {


        public SalesInvoiceDetailsSpecifications(int id) : base(i => i.Id == id)
        {



            AddProjection(i => new SalesInvoiceDetailsDto
            {
                Id = i.Id,
                InvoiceDate = i.InvoiceDate,
                CustomerId = i.CustomerId,
                CustomerName = i.Customer != null ? i.Customer.FullName : null,
                CreatedByEmployeeName = i.CreatedByEmployee != null ? i.CreatedByEmployee.FullName : null,
                TotalAmount = i.TotalAmount,
                InvoiceDiscount = i.InvoiceDiscount,
                DeliveryFee = i.DeliveryFee,
                PaymentMethod = i.PaymentMethod,
                Notes = i.Notes,

                Items = i.Items!.Select(item => new SalesInvoiceItemDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product != null ? item.Product.Name : "Unknown",
                    QuantitySold = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount
                }).ToList()
            });

        }
    }
}
