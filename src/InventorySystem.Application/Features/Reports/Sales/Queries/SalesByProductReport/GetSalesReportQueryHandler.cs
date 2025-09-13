using MediatR;
using Project.Application.Features.Reports.Sales.Dtos;
using Project.Application.Features.Reports.Sales.Interfaces;

namespace Project.Application.Features.Reports.Sales.Queries.Sales
{
    public class GetSalesReportQueryHandler : IRequestHandler<GetSalesByProductReportQuery, IEnumerable<SalesByProductReportDto>>
    {



        private readonly ISalesReportService _salesReportService;


        public GetSalesReportQueryHandler(ISalesReportService salesReportService)
        {

            this._salesReportService = salesReportService;
        }

        public async Task<IEnumerable<SalesByProductReportDto>> Handle(GetSalesByProductReportQuery request, CancellationToken cancellationToken)
        {




            var result = await _salesReportService.GenerateReportAsync(request, cancellationToken);

            return result;




        }
    }
}
