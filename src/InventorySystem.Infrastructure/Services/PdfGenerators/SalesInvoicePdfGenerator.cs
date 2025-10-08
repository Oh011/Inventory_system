using InventorySystem.Application.Common.Interfaces.PdfGenerators;
using InventorySystem.Application.Features.SalesInvoice.Dtos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Infrastructure.Services
{
    internal class SalesInvoicePdfGenerator : ISalesInvoicePdfGenerator
    {
        public byte[] GenerateSalesInvoicePdf(SalesInvoiceDetailsDto invoice)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Text($"Sales Invoice #{invoice.Id}")
                        .SemiBold().FontSize(18).FontColor(Colors.Blue.Medium);

                    page.Content().Column(col =>
                    {
                        col.Spacing(10);

                        col.Item().Text($"Customer: {invoice.CustomerName ?? "Walk-in"}");
                        col.Item().Text($"Invoice Date: {invoice.InvoiceDate:yyyy-MM-dd}");
                        col.Item().Text($"Created By: {invoice.CreatedByEmployeeName ?? "Unknown"}");
                        col.Item().Text($"Payment Method: {invoice.PaymentMethod}");
                        col.Item().Text($"Total Before Discount: {invoice.TotalAmount:C}");
                        col.Item().Text($"Discount: {(invoice.InvoiceDiscount ?? 0):C}");
                        col.Item().Text($"Delivery Fee: {(invoice.DeliveryFee ?? 0):C}");
                        col.Item().Text($"Final Total: {invoice.FinalTotal:C}")
                            .FontSize(14).Bold().FontColor(Colors.Green.Medium);

                        if (!string.IsNullOrWhiteSpace(invoice.Notes))
                            col.Item().Text($"Notes: {invoice.Notes}").Italic();

                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);  // #
                                columns.RelativeColumn(2);   // Product
                                columns.ConstantColumn(60);  // Qty
                                columns.ConstantColumn(80);  // Unit Price
                                columns.ConstantColumn(80);  // Discount
                                columns.ConstantColumn(80);  // Subtotal
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("#").SemiBold();
                                header.Cell().Text("Product").SemiBold();
                                header.Cell().Text("Qty").SemiBold();
                                header.Cell().Text("Unit Price").SemiBold();
                                header.Cell().Text("Discount").SemiBold();
                                header.Cell().Text("Subtotal").SemiBold();
                            });

                            int index = 1;
                            foreach (var item in invoice.Items)
                            {
                                table.Cell().Text(index++);
                                table.Cell().Text(item.ProductName);
                                table.Cell().Text(item.QuantitySold.ToString());
                                table.Cell().Text($"{item.UnitPrice:C}");
                                table.Cell().Text($"{item.Discount:C}");
                                table.Cell().Text($"{item.Subtotal:C}");
                            }
                        });

                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                        col.Item().Text("Thank you for your purchase!").Italic().FontSize(10);
                    });
                });
            }).GeneratePdf();
        }
    }
}
