using Project.Application.Features.SalesInvoice.Commands.Create;
using Project.Application.Features.SalesInvoice.Dtos;
using Project.Application.Features.SalesInvoice.Queries.GetAll;
using Shared.Results;

namespace Project.Application.Features.SalesInvoice.Interfaces
{
    public interface ISalesInvoiceService
    {


        Task<SalesInvoiceDetailsDto> GetInvoiceById(int id);
        Task<PaginatedResult<SalesInvoiceSummaryDto>> GetAllInvoices(GetSalesInvoicesQuery query);

        Task<int> CreateSalesInvoice(CreateSalesInvoiceCommand command);
    }
}
