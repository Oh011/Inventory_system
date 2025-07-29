using Domain.Entities;
using MediatR;
using Project.Application.Common.Enums.SortOptions;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Features.Dashboard.Dtos;
using Project.Application.Features.Inventory.Queries.GetLowStock;
using Project.Application.Features.Notifications.Interfaces;
using Project.Application.Features.Notifications.Queries.GetUserNotifications;
using Project.Application.Features.Products.Intrefaces;
using Project.Application.Features.PurchaseOrders.Interfaces;
using Project.Application.Features.PurchaseOrders.Queries.GetAll;
using Project.Application.Features.SalesInvoice.Interfaces;
using Project.Application.Features.SalesInvoice.Queries.GetAll;

namespace Project.Application.Features.Dashboard.Queries
{
    internal class GetAdminDashboardQueryHandler : IRequestHandler<GetAdminDashboardQuery, DashboardDto>
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        private readonly ISalesInvoiceService _salesInvoiceService;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IUserNotificationsService _userNotificationsService;
        private readonly ICurrentUserService _currentUserService;


        public GetAdminDashboardQueryHandler(ICurrentUserService currentUserService, IUnitOfWork unitOfWork, IUserNotificationsService userNotificationsService, IPurchaseOrderService purchaseOrderService, ISalesInvoiceService salesInvoiceService, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _userNotificationsService = userNotificationsService;
            _purchaseOrderService = purchaseOrderService;
            _salesInvoiceService = salesInvoiceService;
            _productService = productService;
        }
        public async Task<DashboardDto> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
        {


            var dashboard = new DashboardDto();


            var productRepository = _unitOfWork.GetRepository<Product, int>();

            var customersRepository = _unitOfWork.GetRepository<Customer, int>();

            var employeesRepository = _unitOfWork.GetRepository<Employee, int>();

            dashboard.TotalProducts = await productRepository.CountAsync(p => p.IsDeleted == false);

            dashboard.TotalStockValue = (await productRepository.ListAsync
                (p => p.IsDeleted == false, p => p.QuantityInStock * p.CostPrice)).Sum();


            dashboard.TotalCustomers = await customersRepository.CountAsync(c => c.IsDeleted == false);

            dashboard.TotalEmployees = await employeesRepository.CountAsync(c => c.IsDeleted == false);



            var lowStockProducts = (await _productService
                .GetLowStockProductsAsync(new GetLowStockQuery
                {
                    OnlyCritical = false,
                    PageIndex = 1,
                    pageSize = 5,

                }));


            dashboard.LowStockProducts = lowStockProducts.Items.ToList();
            dashboard.LowStockCount = lowStockProducts.TotalCount;




            var sales = await _salesInvoiceService.GetAllInvoices(

                new GetSalesInvoicesQuery()
                {


                    FromDate = DateTime.Today,
                    ToDate = DateTime.Now,
                    PageIndex = 1,
                    pageSize = 5,
                    salesInvoiceSortOptions = SalesInvoiceSortOptions.DateDesc,
                }

                );


            dashboard.RecentSalesInvoices = sales.Items.Take(5).ToList();
            dashboard.SalesTodayTotal = sales.Items.Sum(i => i.FinalTotal); // ✅ Correct



            var orders = await _purchaseOrderService.GetAllPurchaseOrders(

                new GetPurchaseOrdersQuery()
                {

                    PurchaseOrderSortOptions = PurchaseOrderSortOptions.OrderDateDesc,
                    PageIndex = 1,
                    pageSize = 5
                }
                );


            dashboard.RecentPurchaseOrders = orders.Items.ToList();


            var notifications = await _userNotificationsService.GetUserNotifications(

                new GetUserNotificationsQuery(_currentUserService.UserId)
                {

                    PageIndex = 1,
                    pageSize = 5
                }


                );


            dashboard.RecentNotifications = notifications.Items.ToList();

            return dashboard;

        }
    }
}
