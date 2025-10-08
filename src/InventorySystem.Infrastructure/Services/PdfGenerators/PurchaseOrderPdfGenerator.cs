using InventorySystem.Application.Common.Interfaces.PdfGenerators;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace Infrastructure.Services
{
    internal class PurchaseOrderPdfGenerator : IPurchaseOrderPdfGenerator
    {

        public byte[] GenerateCreatedOrderPdf(PurchaseOrderDetailDto order)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // Header
                    page.Header().Text($"Purchase Order #{order.Id}")
                        .SemiBold().FontSize(18).FontColor(Colors.Blue.Medium);

                    page.Content().Column(col =>
                    {
                        col.Spacing(10);

                        // Order Info
                        col.Item().Text($"Supplier: {order.SupplierName}");
                        col.Item().Text($"Order Date: {order.OrderDate:yyyy-MM-dd}");
                        col.Item().Text($"Expected Date: {order.ExpectedDate:yyyy-MM-dd}");

                        // Notes (if any)
                        if (!string.IsNullOrWhiteSpace(order.Notes))
                            col.Item().Text($"Notes: {order.Notes}");

                        // Delivery fee
                        col.Item().Text($"Delivery Fee: {order.DeliveryFee:C}");

                        // Total & Grand Total
                        col.Item().Text($"Total Ordered Value: {order.TotalAmount:C}")
                            .SemiBold().FontSize(14);
                        col.Item().Text($"Grand Total: {order.GrandTotal:C}")
                            .SemiBold().FontSize(14);

                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        // Table of items
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);   // #
                                columns.RelativeColumn(2);    // Product Name
                                columns.RelativeColumn();     // QuantitySold
                                columns.RelativeColumn();     // Unit Cost
                                columns.RelativeColumn();     // Subtotal
                            });

                            // Header
                            table.Header(header =>
                            {
                                header.Cell().Text("#").SemiBold();
                                header.Cell().Text("Product").SemiBold();
                                header.Cell().Text("Qty").SemiBold();
                                header.Cell().Text("Unit Price").SemiBold();
                                header.Cell().Text("Subtotal").SemiBold();
                            });

                            int index = 1;
                            foreach (var item in order.OrderItems)
                            {
                                table.Cell().Text(index++);
                                table.Cell().Text(item.ProductName);
                                table.Cell().Text(item.QuantityOrdered.ToString());
                                table.Cell().Text($"{item.UnitCost:C}");
                                table.Cell().Text($"{item.Subtotal:C}");
                            }
                        });

                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        // Footer message
                        col.Item().Text("Please ensure timely delivery. Thank you.")
                            .Italic().FontSize(10);
                    });
                });
            }).GeneratePdf();
        }


        public byte[] GenerateReceivedOrderPdf(PurchaseOrderDetailDto order)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // Header with dynamic status color
                    page.Header().Text($"Purchase Order #{order.Id} - {order.Status}")
                        .SemiBold().FontSize(18)
                        .FontColor(order.Status == "Fully Received" ? Colors.Green.Medium :
                                   order.Status == "Partially Received" ? Colors.Orange.Medium :
                                   Colors.Red.Medium);

                    page.Content().Column(col =>
                    {
                        col.Spacing(10);

                        // Order Info
                        col.Item().Text($"Supplier: {order.SupplierName}");
                        col.Item().Text($"Order Date: {order.OrderDate:yyyy-MM-dd}");
                        col.Item().Text($"Expected Date: {order.ExpectedDate:yyyy-MM-dd}");

                        // Notes & Delivery Fee
                        if (!string.IsNullOrWhiteSpace(order.Notes))
                            col.Item().Text($"Notes: {order.Notes}");

                        col.Item().Text($"Delivery Fee: {order.DeliveryFee:C}");

                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        // Order Items Table
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);   // #
                                columns.RelativeColumn(2);    // Product Name
                                columns.RelativeColumn();     // Ordered Qty
                                columns.RelativeColumn();     // Ordered Value
                                columns.RelativeColumn();     // Received Qty
                                columns.RelativeColumn();     // Received Value
                                columns.RelativeColumn();     // Unit Cost
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("#").SemiBold();
                                header.Cell().Text("Product").SemiBold();
                                header.Cell().Text("Ordered Qty").SemiBold();
                                header.Cell().Text("Ordered Value").SemiBold();
                                header.Cell().Text("Received Qty").SemiBold();
                                header.Cell().Text("Received Value").SemiBold();
                                header.Cell().Text("Unit Price").SemiBold();
                            });

                            int index = 1;
                            foreach (var item in order.OrderItems)
                            {
                                table.Cell().Text(index++);
                                table.Cell().Text(item.ProductName);
                                table.Cell().Text(item.QuantityOrdered.ToString());
                                table.Cell().Text($"{item.Subtotal:C}"); // Ordered value
                                table.Cell().Text(item.QuantityReceived.ToString());
                                table.Cell().Text($"{item.UnitCost * item.QuantityReceived:C}"); // Received value
                                table.Cell().Text($"{item.UnitCost:C}");
                            }
                        });

                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        // Totals
                        col.Item().Text($"Total Ordered Value: {order.TotalAmount:C}")
                            .SemiBold().FontSize(14);
                        col.Item().Text($"Total Received Value: {order.TotalReceivedValue:C}")
                            .SemiBold().FontSize(14);
                        col.Item().Text($"Total QuantitySold Received: {order.TotalQuantityReceived}");

                        col.Item().Text("This document reflects items received for this order.")
                            .Italic().FontSize(10);
                    });
                });
            }).GeneratePdf();
        }




    }
}
