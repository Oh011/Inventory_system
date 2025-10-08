using InventorySystem.Application.Features.SalesReturn.Dtos;
using MediatR;
using Shared.Results;

namespace InventorySystem.Application.Features.SalesReturn.Commands.Create
{
    public class CreateSalesReturnCommand : IRequest<Result<SalesReturnDto>>
    {

        public int SalesInvoiceId { get; set; }

        public string Reason { get; set; } = string.Empty;

        public List<CreateSalesReturnItemDto> Items { get; set; } = new();
    }
}
