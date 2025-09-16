using Domain.Specifications;
using InventorySystem.Application.Common.Enums.SortOptions;
using InventorySystem.Application.Features.SalesInvoice.Dtos;
using InventorySystem.Application.Features.SalesInvoice.Queries.GetAll;

using salesInvoice = Domain.Entities.SalesInvoice;
namespace InventorySystem.Application.Features.SalesInvoice.specifications
{
    public class SalesInvoiceSpecifications : ProjectionSpecifications<salesInvoice, SalesInvoiceSummaryDto>
    {
        public SalesInvoiceSpecifications(GetSalesInvoicesQuery query) :
            base(
                i =>
                    (!query.CustomerId.HasValue || i.CustomerId == query.CustomerId.Value) &&
                    (!query.FromDate.HasValue || i.InvoiceDate >= query.FromDate.Value) &&
                    (!query.ToDate.HasValue || i.InvoiceDate <= query.ToDate.Value) &&
                    (!query.PaymentMethod.HasValue || i.PaymentMethod == query.PaymentMethod.Value) &&
                    (string.IsNullOrEmpty(query.Search) ||
                        i.Customer!.FullName.Contains(query.Search) ||
                        i.CreatedByEmployee!.FullName.Contains(query.Search))
            )
        {
            AddInclude(i => i.Customer);
            AddInclude(i => i.CreatedByEmployee);

            AddProjection(i => new SalesInvoiceSummaryDto
            {
                Id = i.Id,
                InvoiceDate = i.InvoiceDate,
                CustomerId = i.CustomerId,
                CustomerName = i.Customer != null ? i.Customer.FullName : null,
                CreatedByEmployeeName = i.CreatedByEmployee != null ? i.CreatedByEmployee.FullName : null,
                FinalTotal = i.FinalTotal,
                PaymentMethod = i.PaymentMethod.ToString()
            });

            switch (query.salesInvoiceSortOptions)
            {
                case SalesInvoiceSortOptions.DateAsc:
                    SetResultOrderBy(i => i.InvoiceDate);
                    break;
                case SalesInvoiceSortOptions.DateDesc:
                    SetResultOrderByDescending(i => i.InvoiceDate);
                    break;
                case SalesInvoiceSortOptions.FinalTotalAsc:
                    SetResultOrderBy(i => i.FinalTotal);
                    break;
                case SalesInvoiceSortOptions.FinalTotalDesc:
                    SetResultOrderByDescending(i => i.FinalTotal);
                    break;

                default:
                    SetResultOrderByDescending(i => i.InvoiceDate);
                    break;
            }

            ApplyPagination(query.PageIndex, query.pageSize);
        }
    }

}
