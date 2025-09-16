
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using InventorySystem.Application.Features.Categories.Interfaces;
using InventorySystem.Application.Features.Employees.Interfcaes;
using InventorySystem.Application.Features.Notifications.Interfaces;
using InventorySystem.Application.Features.Products.Intrefaces;
using InventorySystem.Application.Features.PurchaseOrders.Interfaces;
using InventorySystem.Application.Features.Reports.Sales.Interfaces;
using InventorySystem.Application.Features.SalesInvoice.Interfaces;
using InventorySystem.Application.Features.Suppliers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InventorySystem.Services.DependecyInjcetion
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddServices(this IServiceCollection services)
        {


            services.AddScoped<IStockEventService, StockEventService>();



            services.AddScoped<IInventoryService, InventoryService>();

            services.AddScoped<ISalesReportService, SalesReportService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IUserNotificationsService, UserNotificationsService>();

            services.AddScoped<ISalesInvoiceService, SalesInvoiceService>();


            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ISupplierService, SupplierService>();

            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();


            return services;


        }
    }
}
