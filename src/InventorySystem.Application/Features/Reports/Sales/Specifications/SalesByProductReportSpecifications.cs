using Domain.Entities;
using Domain.Specifications;
using InventorySystem.Application.Features.Reports.Sales.FilterParameters;
using InventorySystem.Application.Features.Reports.Sales.SortOptions;
using Project.Application.Features.Reports.Sales.Dtos;

namespace Project.Application.Features.Reports.Sales.Specifications
{
    public class SalesByProductReportSpecifications : GroupSpecification<SalesInvoiceItem, ProductSalesGroupKey, SalesByProductReportDto>
    {
        public SalesByProductReportSpecifications(SalesByProductReportFilterParams query)
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

            AddGroupSelector(group => new SalesByProductReportDto
            {
                ProductId = group.Key.ProductId,
                ProductName = group.Key.ProductName,
                CategoryName = group.Key.CategoryName,
                SellingPrice = group.Key.UnitPrice,
                UnitsSold = group.Sum(x => x.Quantity)
            });




            switch (query.ReportSortOptions)
            {
                case SalesByProductReportSortOptions.SellingPriceAsc:
                    SetResultOrderBy(x => x.SellingPrice);
                    break;

                case SalesByProductReportSortOptions.SellingPriceDesc:
                    SetResultOrderByDescending(x => x.SellingPrice);
                    break;

                case SalesByProductReportSortOptions.UnitsSoldAsc:
                    SetResultOrderBy(x => x.UnitsSold);
                    break;

                case SalesByProductReportSortOptions.UnitsSoldDesc:
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


    }

}
