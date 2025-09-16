using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Features.Reports.Sales.Dtos;
using InventorySystem.Application.Features.Reports.Sales.Specifications;
using MediatR;
using salesInvoice = Domain.Entities.SalesInvoice;
namespace InventorySystem.Application.Features.Reports.Sales.Queries.SalesByCustomerReport
{
    internal class GetSalesByCustomerReportQueryHandler : IRequestHandler<GetSalesByCustomerReportQuery, IEnumerable<SalesByCustomerReportDto>>
    {

        private readonly IUnitOfWork unitOfWork;


        public GetSalesByCustomerReportQueryHandler(IUnitOfWork unitOfWork)
        {

            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<SalesByCustomerReportDto>> Handle(GetSalesByCustomerReportQuery request, CancellationToken cancellationToken)
        {

            var specifications = new SalesByCustomerReportSpecification(request);

            var repository = unitOfWork.GetRepository<salesInvoice, int>();

            var result = await repository.GetAllWithGrouping(specifications);


            return result;
        }
    }
}
