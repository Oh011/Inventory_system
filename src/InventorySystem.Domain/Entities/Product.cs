using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {



        public string Name { get; set; }


        public string? Description { get; set; }


        public string? Barcode { get; set; }


        public UnitType Unit { get; set; }


        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int QuantityInStock { get; set; } = 0;

        public int MinimumStock { get; set; }



        public bool LowStock => QuantityInStock < MinimumStock;
        public bool OutOfStock => QuantityInStock == 0;
        public string? ProductImageUrl { get; set; }


        public int? CategoryId { get; set; } = null;


        [InverseProperty(nameof(Category.Products))]
        public Category? Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public DateTime? LastLowStockNotifiedAt { get; set; }



        public ICollection<PurchaseOrderItem>? PurchaseOrders { get; set; } = new List<PurchaseOrderItem>();


        public ICollection<SalesInvoiceItem>? SalesInvoices { get; set; } = new List<SalesInvoiceItem>();






        public void DecreaseStock(int quantity)
        {
            if (quantity > this.QuantityInStock)
            {

                throw new DomainException(
         $"Insufficient stock for product '{Name}'. Requested {quantity}, available {QuantityInStock}."
     );
            }

            this.QuantityInStock -= quantity;
        }



        public void IncreaseStock(int quantity)
        {

            if (quantity < 0)
            {

                throw new DomainException(
           $"Invalid stock increment value for product '{Name}'. Received {quantity}."
       );
            }

            this.QuantityInStock = this.QuantityInStock + quantity;
        }
    }
}
