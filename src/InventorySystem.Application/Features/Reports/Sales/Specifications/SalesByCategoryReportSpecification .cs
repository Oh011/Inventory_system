using Domain.Entities;
using Domain.Specifications;
using InventorySystem.Application.Features.Reports.Sales.Dtos;
using InventorySystem.Application.Features.Reports.Sales.FilterParameters;
using InventorySystem.Application.Features.Reports.Sales.SortOptions;

namespace InventorySystem.Application.Features.Reports.Sales.Specifications
{
    public class SalesByCategoryReportSpecification
      : GroupSpecification<SalesInvoiceItem, CategorySalesGroupKey, SalesByCategoryReportDto>
    {
        public SalesByCategoryReportSpecification(SalesByCategoryFilterParams query)
            : base(x =>
                x.SalesInvoice.InvoiceDate >= query.FromDate &&
                x.SalesInvoice.InvoiceDate <= query.ToDate)
        {

            AddGroupBy(x => new CategorySalesGroupKey
            {
                CategoryId = x.Product.CategoryId ?? 0,
                CategoryName = x.Product.Category != null
                    ? x.Product.Category.Name
                    : "Uncategorized"
            });

            // 2. Select into DTO
            AddGroupSelector(group => new SalesByCategoryReportDto
            {
                CategoryId = group.Key.CategoryId,
                CategoryName = group.Key.CategoryName,
                UnitsSold = group.Sum(x => x.Quantity),
                TotalRevenue = group.Sum(x => x.Quantity * x.UnitPrice)
            });

            // 3. Sorting
            ApplySorting(query.ReportSortOptions);

            // 4. Pagination (Top N categories)
            if (query.TopCount.HasValue)
            {
                ApplyPagination(1, query.TopCount.Value);
            }
        }

        private void ApplySorting(SalesByCategoryReportSortOptions? sortOption)
        {
            switch (sortOption)
            {
                case SalesByCategoryReportSortOptions.RevenueAsc:
                    SetResultOrderBy(x => x.TotalRevenue);
                    break;
                case SalesByCategoryReportSortOptions.RevenueDesc:
                    SetResultOrderByDescending(x => x.TotalRevenue);
                    break;
                case SalesByCategoryReportSortOptions.UnitsSoldAsc:
                    SetResultOrderBy(x => x.UnitsSold);
                    break;
                case SalesByCategoryReportSortOptions.UnitsSoldDesc:
                default:
                    SetResultOrderByDescending(x => x.UnitsSold);
                    break;
            }
        }
    }



    public class CategorySalesGroupKey
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;
    }


}
