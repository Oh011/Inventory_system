using Project.Application.Common.Interfaces.PdfGenerators;
using Project.Application.Features.PurchaseOrders.Dtos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace Infrastructure.Services
{
    internal class PurchaseOrderPdfGenerator : IPurchaseOrderPdfGenerator
    {


        public byte[] GenerateCreatedOrderPdf(PurchaseOrderResultDto order)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));




                    page.Header().Text($"Purchase Order #{order.Id}")
                        .SemiBold().FontSize(18).FontColor(Colors.Blue.Medium);

                    page.Content().Column(col =>
                    {
                        col.Spacing(10);

                        col.Item().Text($"Supplier: {order.SupplierName}");
                        col.Item().Text($"Order Date: {order.OrderDate:yyyy-MM-dd}");
                        col.Item().Text($"Expected Date: {order.ExpectedDate:yyyy-MM-dd}");
                        col.Item().Text($"Total: {order.TotalAmount:C}");

                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);  // #
                                columns.RelativeColumn(2);   // Product Name
                                columns.RelativeColumn();    // Quantity
                                columns.RelativeColumn();    // Unit Cost
                                columns.RelativeColumn();    // Total
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
                                table.Cell().Text($"{item.QuantityOrdered * item.UnitCost:C}");
                            }
                        });

                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        col.Item().Text("Please ensure timely delivery. Thank you.")
                            .Italic().FontSize(10);
                    });
                });
            }).GeneratePdf();
        }

        public byte[] GenerateReceivedOrderPdf(PurchaseOrderResultDto order)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Text($"Purchase Order #{order.Id} - Received")
                        .SemiBold().FontSize(18).FontColor(Colors.Green.Medium);

                    page.Content().Column(col =>
                    {
                        col.Spacing(10);

                        col.Item().Text($"Supplier: {order.SupplierName}");
                        col.Item().Text($"Order Date: {order.OrderDate:yyyy-MM-dd}");
                        col.Item().Text($"Expected Date: {order.ExpectedDate:yyyy-MM-dd}");

                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);  // #
                                columns.RelativeColumn(2);   // Product Name
                                columns.RelativeColumn();    // Ordered Qty
                                columns.RelativeColumn();    // Received Qty
                                columns.RelativeColumn();    // Unit Cost
                                columns.RelativeColumn();    // Subtotal
                            });

                            // Header
                            table.Header(header =>
                            {
                                header.Cell().Text("#").SemiBold();
                                header.Cell().Text("Product").SemiBold();
                                header.Cell().Text("Ordered").SemiBold();
                                header.Cell().Text("Received").SemiBold();
                                header.Cell().Text("Unit Price").SemiBold();
                                header.Cell().Text("Subtotal").SemiBold();
                            });

                            int index = 1;
                            foreach (var item in order.OrderItems)
                            {
                                table.Cell().Text(index++);
                                table.Cell().Text(item.ProductName);
                                table.Cell().Text(item.QuantityOrdered.ToString());
                                table.Cell().Text(item.QuantityReceived.ToString());
                                table.Cell().Text($"{item.UnitCost:C}");
                                table.Cell().Text($"{item.QuantityReceived * item.UnitCost:C}");
                            }
                        });

                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        var totalReceived = order.OrderItems.Sum(i => i.QuantityReceived * i.UnitCost);

                        col.Item().Text($"Total Received: {totalReceived:C}")
                            .SemiBold().FontSize(14).FontColor(Colors.Black);

                        col.Item().Text("This document reflects items received for this order.")
                            .Italic().FontSize(10);
                    });
                });
            }).GeneratePdf();
        }


    }
}
