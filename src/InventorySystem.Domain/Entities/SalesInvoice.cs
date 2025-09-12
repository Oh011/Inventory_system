using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects.SalesInvoice.Domain.ValueObjects;

namespace Domain.Entities
{
    public class SalesInvoice : BaseEntity
    {


        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;


        public int? CreatedByEmployeeId { get; set; }

        public Employee? CreatedByEmployee { get; set; }


        public decimal TotalAmount { get; set; } // Total before discounts and delivery

        public decimal? InvoiceDiscount { get; set; }

        public decimal? DeliveryFee { get; set; }

        public decimal FinalTotal =>
            TotalAmount - (InvoiceDiscount ?? 0) + (DeliveryFee ?? 0);

        public PaymentMethod PaymentMethod { get; set; }

        public string? Notes { get; set; }


        public int? CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public ICollection<SalesInvoiceItem>? Items { get; set; } = new List<SalesInvoiceItem>();





        public decimal AddItems(IEnumerable<SalesInvoiceItemData> items)
        {
            decimal total = 0;

            foreach (var item in items)
            {
                var invoiceItem = new SalesInvoiceItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.QuantitySold,
                    UnitPrice = item.SellingPrice,
                    Discount = item.Discount
                };

                Items.Add(invoiceItem);
                total += invoiceItem.Subtotal; // (UnitPrice * Quantity) - Discount
            }

            TotalAmount = total;

            return total;
        }

    }
}
