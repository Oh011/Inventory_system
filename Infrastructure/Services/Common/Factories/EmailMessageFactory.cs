using Microsoft.Extensions.Options;
using Project.Application.Common.Factories;
using Shared.Dtos;
using Shared.Options;

namespace Infrastructure.Services.Common.Factories
{
    public class EmailMessageFactory : IEmailMessageFactory
    {
        private readonly IOptions<SmtpOptions> _options;

        public EmailMessageFactory(IOptions<SmtpOptions> options)
        {
            _options = options;
        }

        public EmailMessage CreateOrderCanceledEmail(int orderId, string supplierName, string supplierEmail)
        {

            return new EmailMessage
            {

                To = supplierEmail,
                Subject = $"Purchase Order #{orderId} Canceled",
                Body = $"Dear {supplierName},\n\nPlease be informed that Purchase Order #{orderId} has been canceled."

            };
        }

        public EmailMessage CreateOrderReceivedEmail(int orderId, string supplierName, string supplierEmail, byte[]? pdfBytes = null)
        {
            throw new NotImplementedException();
        }



        public EmailMessage CreatePurchaseOrderEmail(int orderId, string supplierName, string supplierEmail, byte[]? pdfBytes = null)
        {


            var emailMessage = new EmailMessage
            {
                To = supplierEmail,
                Subject = $"New Purchase Order with Id : {orderId}",
                Body = $"Dear Supplier,\n\nA new purchase order (ID: {orderId}) has been created. Please check the system for details.",
                Attachments = new List<EmailAttachment>
                {


                        new EmailAttachment
                        {
                            FileName = $"PurchaseOrder_{orderId}.pdf",
                            ContentType = "application/pdf",
                            Content = pdfBytes
                        }
                }
            };

            return emailMessage;
        }
    }

}
