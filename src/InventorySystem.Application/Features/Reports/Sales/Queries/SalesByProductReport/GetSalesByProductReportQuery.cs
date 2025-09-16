using InventorySystem.Application.Features.Reports.Sales.FilterParameters;
using MediatR;
using InventorySystem.Application.Features.Reports.Sales.Dtos;

namespace InventorySystem.Application.Features.Reports.Sales.Queries.Sales
{
    public class GetSalesByProductReportQuery : SalesByProductReportFilterParams, IRequest<IEnumerable<SalesByProductReportDto>>
    {

    }

}
