using Domain.Entities;
using Domain.Specifications;
using Project.Application.Common.Enums.SortOptions;
using Project.Application.Features.Reports.Sales.Dtos;

namespace Project.Application.Features.Reports.Sales.Specifications
{
    public class SalesReportSpecifications : GroupSpecification<SalesInvoiceItem, ProductSalesGroupKey, SalesReportDto>
    {
        public SalesReportSpecifications(SalesReportFilterParams query)
          : base(x =>
              x.SalesInvoice.InvoiceDate >= query.FromDate &&
              x.SalesInvoice.InvoiceDate <= query.ToDate &&
              (!query.ProductId.HasValue || x.ProductId == query.ProductId.Value) &&
              (!query.CategoryId.HasValue || x.Product.CategoryId == query.CategoryId.Value))
        {
            AddGroupBy(x => new ProductSalesGroupKey
            {
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                CategoryName = x.Product.Category.Name,
                UnitPrice = x.UnitPrice
            });

            AddGroupSelector(group => new SalesReportDto
            {
                ProductId = group.Key.ProductId,
                ProductName = group.Key.ProductName,
                CategoryName = group.Key.CategoryName,
                SellingPrice = group.Key.UnitPrice,
                UnitsSold = group.Sum(x => x.Quantity)
            });




            switch (query.ReportSortOptions)
            {
                case SalesReportSortOptions.SellingPriceAsc:
                    SetResultOrderBy(x => x.SellingPrice);
                    break;

                case SalesReportSortOptions.SellingPriceDec:
                    SetResultOrderByDescending(x => x.SellingPrice);
                    break;

                case SalesReportSortOptions.UnitsSoldAsc:
                    SetResultOrderBy(x => x.UnitsSold);
                    break;

                case SalesReportSortOptions.UnitsSoldDesc:
                default: // Default fallback: top selling
                    SetResultOrderByDescending(x => x.UnitsSold);
                    break;
            }


            ApplyPagination(1, query.TopCount ?? 10);
        }
    }



    public class ProductSalesGroupKey
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public string? CategoryName { get; set; }
        public decimal UnitPrice { get; set; }

        // Important: override Equals & GetHashCode if using LINQ outside EF
    }

}
