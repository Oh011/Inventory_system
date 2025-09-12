using Domain.Entities;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Features.Reports.Sales.Dtos;
using Project.Application.Features.Reports.Sales.Interfaces;
using Project.Application.Features.Reports.Sales.Specifications;

namespace Project.Services
{
    public class SalesReportService : ISalesReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SalesReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SalesReportDto>> GenerateReportAsync(SalesReportFilterParams query, CancellationToken cancellationToken = default)
        {
            var repository = _unitOfWork.GetRepository<SalesInvoiceItem, int>();
            var specifications = new SalesReportSpecifications(query);
            var result = await repository.GetAllWithGrouping(specifications);
            return result;
        }
    }

}
