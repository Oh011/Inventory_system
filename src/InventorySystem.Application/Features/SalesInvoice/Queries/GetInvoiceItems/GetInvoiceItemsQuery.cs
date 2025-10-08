using InventorySystem.Application.Features.SalesInvoice.Dtos;
using MediatR;

namespace InventorySystem.Application.Features.SalesInvoice.Queries.GetInvoiceItems
{
    public record GetInvoiceItemsQuery(int Id) : IRequest<IEnumerable<SalesInvoiceItemDto>>;

}
