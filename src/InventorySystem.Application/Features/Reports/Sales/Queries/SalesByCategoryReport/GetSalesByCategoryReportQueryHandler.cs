using Domain.Entities;
using InventorySystem.Application.Features.Reports.Sales.Dtos;
using InventorySystem.Application.Features.Reports.Sales.Specifications;
using MediatR;
using InventorySystem.Application.Common.Interfaces.Repositories;

namespace InventorySystem.Application.Features.Reports.Sales.Queries.SalesByCategoryReport
{
    internal class GetSalesByCategoryReportQueryHandler : IRequestHandler<GetSalesByCategoryReportQuery, IEnumerable<SalesByCategoryReportDto>>
    {

        private readonly IUnitOfWork unitOfWork;


        public GetSalesByCategoryReportQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<SalesByCategoryReportDto>> Handle(GetSalesByCategoryReportQuery request, CancellationToken cancellationToken)
        {

            var specifications = new SalesByCategoryReportSpecification(request);

            var repository = unitOfWork.GetRepository<SalesInvoiceItem, int>();

            var result = await repository.GetAllWithGrouping(specifications);


            return result;

        }
    }
}
