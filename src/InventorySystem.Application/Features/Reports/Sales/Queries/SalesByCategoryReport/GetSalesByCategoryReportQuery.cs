using InventorySystem.Application.Features.Reports.Sales.Dtos;
using InventorySystem.Application.Features.Reports.Sales.FilterParameters;
using MediatR;

namespace InventorySystem.Application.Features.Reports.Sales.Queries.SalesByCategoryReport
{
    public class GetSalesByCategoryReportQuery : SalesByCategoryFilterParams, IRequest<IEnumerable<SalesByCategoryReportDto>>
    {


    }
}
