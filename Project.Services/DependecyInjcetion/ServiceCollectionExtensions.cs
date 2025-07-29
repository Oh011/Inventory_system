using Microsoft.Extensions.DependencyInjection;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Features.Categories.Interfaces;
using Project.Application.Features.Employees.Interfcaes;
using Project.Application.Features.Notifications.Interfaces;
using Project.Application.Features.Products.Intrefaces;
using Project.Application.Features.PurchaseOrders.Interfaces;
using Project.Application.Features.Reports.Sales.Interfaces;
using Project.Application.Features.SalesInvoice.Interfaces;
using Project.Application.Features.Suppliers.Interfaces;

namespace Project.Services.DependecyInjcetion
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddServices(this IServiceCollection services)
        {


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
