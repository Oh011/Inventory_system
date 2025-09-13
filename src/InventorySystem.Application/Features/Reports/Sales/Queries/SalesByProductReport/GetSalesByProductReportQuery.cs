using InventorySystem.Application.Features.Reports.Sales.FilterParameters;
using MediatR;
using Project.Application.Features.Reports.Sales.Dtos;

namespace Project.Application.Features.Reports.Sales.Queries.Sales
{
    public class GetSalesByProductReportQuery : SalesByProductReportFilterParams, IRequest<IEnumerable<SalesByProductReportDto>>
    {

    }

}
