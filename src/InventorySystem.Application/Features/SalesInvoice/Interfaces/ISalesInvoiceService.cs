using InventorySystem.Application.Features.SalesInvoice.Commands.Create;
using InventorySystem.Application.Features.SalesInvoice.Dtos;
using InventorySystem.Application.Features.SalesInvoice.Queries.GetAll;
using Shared.Results;

namespace InventorySystem.Application.Features.SalesInvoice.Interfaces
{
    public interface ISalesInvoiceService
    {


        Task<IEnumerable<SalesInvoiceItemDto>> GetInvoiceItems(int id);


        Task<IEnumerable<SalesInvoiceItemWithReturnInfoDto>> GetInvoiceItemsWithReturnInfo(int id);
        Task<SalesInvoiceDetailsDto> GetInvoiceById(int id);
        Task<PaginatedResult<SalesInvoiceSummaryDto>> GetAllInvoices(GetSalesInvoicesQuery query);

        Task<int> CreateSalesInvoice(CreateSalesInvoiceCommand command);
    }
}
