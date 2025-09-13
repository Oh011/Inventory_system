using Domain.Entities;
using Domain.Specifications;
using InventorySystem.Application.Features.Reports.Sales.Dtos;
using InventorySystem.Application.Features.Reports.Sales.FilterParameters;
using InventorySystem.Application.Features.Reports.Sales.SortOptions;

namespace InventorySystem.Application.Features.Reports.Sales.Specifications
{


    public class SalesByCustomerReportSpecification
    : GroupSpecification<SalesInvoice, CustomerGroupKey, SalesByCustomerReportDto>
    {
        public SalesByCustomerReportSpecification(SalesByCustomerFilterParams query)
            : base(x =>
                x.InvoiceDate >= query.FromDate &&
                x.InvoiceDate <= query.ToDate &&
                (!query.CustomerId.HasValue || x.CustomerId == query.CustomerId.Value) &&
                (string.IsNullOrEmpty(query.Search) ||
                    x.Customer.FullName.Contains(query.Search) ||
                    x.Customer.Email.Contains(query.Search) ||
                    x.Customer.Phone.Contains(query.Search)))
        {
            // Group key contains all the info we need
            AddGroupBy(x => new CustomerGroupKey
            {
                CustomerId = x.CustomerId ?? 0,
                CustomerName = x.Customer != null ? x.Customer.FullName : "Unknown",
                Phone = x.Customer != null ? x.Customer.Phone : null,
                Email = x.Customer != null ? x.Customer.Email : null
            });

            // Selector maps aggregates to DTO
            AddGroupSelector(group => new SalesByCustomerReportDto
            {
                CustomerId = group.Key.CustomerId,
                CustomerName = group.Key.CustomerName,
                Phone = group.Key.Phone,
                Email = group.Key.Email,
                NumberOfInvoices = group.Count(),
                TotalUnitsPurchased = group.Sum(i => i.Items.Sum(it => it.Quantity)),
                TotalRevenue = group.Sum(i => i.TotalAmount - (i.InvoiceDiscount ?? 0) + (i.DeliveryFee ?? 0))
            });

            // Sorting
            switch (query.SortOption)
            {
                case SalesByCustomerSortOptions.RevenueAsc:
                    SetResultOrderBy(x => x.TotalRevenue);
                    break;
                case SalesByCustomerSortOptions.RevenueDesc:
                    SetResultOrderByDescending(x => x.TotalRevenue);
                    break;
                case SalesByCustomerSortOptions.UnitsPurchasedAsc:
                    SetResultOrderBy(x => x.TotalUnitsPurchased);
                    break;
                case SalesByCustomerSortOptions.UnitsPurchasedDesc:
                    SetResultOrderByDescending(x => x.TotalUnitsPurchased);
                    break;
                case SalesByCustomerSortOptions.NumberOfInvoicesAsc:
                    SetResultOrderBy(x => x.NumberOfInvoices);
                    break;
                case SalesByCustomerSortOptions.NumberOfInvoicesDesc:
                default:
                    SetResultOrderByDescending(x => x.NumberOfInvoices);
                    break;
            }


            ApplyPagination(1, query.TopCount ?? 10);
        }
    }

    // Group key class
    public class CustomerGroupKey
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = default!;
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }


}