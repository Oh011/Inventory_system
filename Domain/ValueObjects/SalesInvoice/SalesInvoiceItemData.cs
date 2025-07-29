using Domain.Exceptions;

namespace Domain.ValueObjects.SalesInvoice
{
    namespace Domain.ValueObjects
    {
        public class SalesInvoiceItemData
        {
            public int ProductId { get; }
            public int QuantitySold { get; }
            public decimal SellingPrice { get; }
            public decimal Discount { get; } // Default to 0 if not set

            public SalesInvoiceItemData(int productId, int quantitySold, decimal sellingPrice, decimal? discount = null)
            {
                if (quantitySold <= 0)
                    throw new DomainException("Quantity must be greater than zero.");

                if (sellingPrice <= 0)
                    throw new DomainException("Selling price must be greater than zero.");

                ProductId = productId;
                QuantitySold = quantitySold;
                SellingPrice = sellingPrice;
                Discount = discount ?? 0;
            }
        }
    }

}
