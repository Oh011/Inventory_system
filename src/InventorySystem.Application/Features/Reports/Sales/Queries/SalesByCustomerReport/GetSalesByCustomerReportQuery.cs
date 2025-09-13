using InventorySystem.Application.Features.Reports.Sales.Dtos;
using InventorySystem.Application.Features.Reports.Sales.FilterParameters;
using MediatR;

namespace InventorySystem.Application.Features.Reports.Sales.Queries.SalesByCustomerReport
{
    public class GetSalesByCustomerReportQuery : SalesByCustomerFilterParams, IRequest<IEnumerable<SalesByCustomerReportDto>>
    {
    }
}
