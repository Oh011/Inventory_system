using Application.Features.Users.Interfaces;
using Domain.Entities;
using Domain.Events.Products;
using Project.Application.Common.Factories;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Common.Notifications.Senders;
using Project.Application.Features.Inventory.Dtos;
using Project.Application.Features.Notifications.Dtos;
using Project.Application.Features.Notifications.Interfaces;

namespace Infrastructure.Notifications.Senders
{
    public class ProductStockDecreasedNotificationSender
    : INotificationSender<ProductStockDecreasedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IUserNotificationsService _userNotificationsService;
        private readonly INotificationService _notificationService;
        private readonly INotificationDtoFactory notificationDtoFactory;

        public ProductStockDecreasedNotificationSender(
            IUnitOfWork unitOfWork,
            IUserService userService,
            IUserNotificationsService userNotificationsService,
            INotificationDtoFactory notificationDtoFactory,
            INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _userNotificationsService = userNotificationsService;
            _notificationService = notificationService;
        }

        public async Task SendAsync(ProductStockDecreasedEvent domainEvent)
        {
            var productIds = domainEvent.AffectedProductIds;

            var repository = _unitOfWork.GetRepository<Product, int>();

            var lowStockProducts = await repository.ListAsync(
                p => productIds.Contains(p.Id) && p.QuantityInStock < p.MinimumStock,
                p => new LowStockProductDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    QuantityInStock = p.QuantityInStock,
                    ReorderThreshold = p.MinimumStock,
                });

            var userIds = await _userService.GetUsersIdByRole(
                new List<string> { "Admin", "Manager", "Warehouse" });

            var createDtos = lowStockProducts
            .Select(p => notificationDtoFactory.CreateLowStockNotification(p, userIds))
            .ToList();

            var createdNotifications = await _userNotificationsService.CreateNotifications(createDtos);

            var group = new LowStockNotificationGroup
            {
                Notifications = createdNotifications.ToList(),
            };

            await _notificationService.NotifyLowStockProducts(group);
        }


    }

}
