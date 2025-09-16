using Domain.Enums;
using MediatR;
using InventorySystem.Application.Features.SalesInvoice.Dtos;

namespace InventorySystem.Application.Features.SalesInvoice.Commands.Create
{
    public class CreateSalesInvoiceCommand : IRequest<SalesInvoiceDetailsDto>
    {

        public int CustomerId { get; set; }
        public int? CreatedByEmployeeId { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;

        public string? Notes { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public decimal? DiscountAmount { get; set; } // Optional flat discount

        public List<SalesInvoiceItemCreateDto> Items { get; set; } = [];
    }
}
