using Domain.Enums;
using InventorySystem.Application.Common.Factories.Interfaces;
using Microsoft.Extensions.Options;
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

        public EmailMessage CreateOrderReceivedEmail(int orderId, string supplierName, string supplierEmail, PurchaseOrderStatus status, byte[]? pdfBytes = null)
        {


            var statusText = status == PurchaseOrderStatus.Received
          ? "Fully Received"
          : status == PurchaseOrderStatus.PartiallyReceived
              ? "Partially Received"
              : status.ToString();


            return new EmailMessage
            {
                To = supplierEmail,
                Subject = $"Purchase Order #{orderId} - {statusText}",
                Body = $@"
                Dear {supplierName},

                Please be informed that Purchase Order #{orderId} has been {statusText.ToLower()}.
                See the attached PDF for details.

                Thank you.",
                Attachments = new List<EmailAttachment>
                            {
                                new EmailAttachment
                                {
                                    FileName = $"PurchaseOrder_{orderId}_{statusText.Replace(" ", "")}.pdf",
                                    ContentType = "application/pdf",
                                    Content = pdfBytes
                                }
                            }
            };


        }



        public EmailMessage CreatePurchaseOrderCreatedEmail(int orderId, string supplierName, string supplierEmail, byte[]? pdfBytes = null)
        {


            var emailMessage = new EmailMessage
            {
                To = supplierEmail,
                Subject = $"New Purchase Order {orderId}",
                Body = $@"
                Dear {supplierName},

                A new purchase order (ID: {orderId}) has been created. 
                Please find the attached PDF for details, and log in to the system for full information.

                Thank you.",

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
