using Shared.Dtos;

namespace Project.Application.Common.Factories
{
    public interface IEmailMessageFactory
    {

        EmailMessage CreatePurchaseOrderCreatedEmail(int orderId, string supplierName, string supplierEmail, byte[]? pdfBytes = null);
        EmailMessage CreateOrderCanceledEmail(int orderId, string supplierName, string supplierEmail);
        EmailMessage CreateOrderReceivedEmail(int orderId, string supplierName, string supplierEmail, byte[]? pdfBytes = null);
    }
}
