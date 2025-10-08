using InventorySystem.Application.Features.SalesInvoice.Dtos;
using MediatR;
using Shared.Results;

namespace InventorySystem.Application.Features.SalesInvoice.Queries.GetInvoiceItemsWithReturn
{
    public record GetInvoiceItemsWithReturnInfoQuery(int id) :
      IRequest<Result<IEnumerable<SalesInvoiceItemWithReturnInfoDto>>>;

}
