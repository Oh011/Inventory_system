using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.SalesInvoice.Dtos
{
    public class SalesInvoiceItemCreateDto
    {
        public int ProductId { get; set; }
        public int QuantitySold { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal? Discount { get; set; } // Optional per item
    }

}
