using Application.Features.Users.Interfaces;
using Domain.Entities;
using Domain.Events.Products;
using InventorySystem.Application.Common.Factories.Interfaces;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using InventorySystem.Application.Common.Notifications.Senders;
using InventorySystem.Application.Features.Inventory.Dtos;
using InventorySystem.Application.Features.Notifications.Dtos;
using InventorySystem.Application.Features.Notifications.Interfaces;

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
            this.notificationDtoFactory = notificationDtoFactory;
        }

        public async Task SendAsync(ProductStockDecreasedEvent domainEvent)
        {
            var productIds = domainEvent.AffectedProductIds;

            var repository = _unitOfWork.GetRepository<Product, int>();

            var products = await repository.FindAsync(
                p => productIds.Contains(p.Id) && p.QuantityInStock < p.MinimumStock &&
                (p.LastLowStockNotifiedAt == null || p.LastLowStockNotifiedAt < DateTime.UtcNow.AddHours(-48)), false
              );

            var userIds = await _userService.GetUsersIdByRole(
                new List<string> { "Admin", "Manager", "Warehouse" });

            var lowStockProducts = products.Select(p => new LowStockProductDto
            {
                ProductId = p.Id,
                ProductName = p.Name,
                ReorderThreshold = p.MinimumStock,
                QuantityInStock = p.QuantityInStock,
            });

            var createDtos = lowStockProducts
            .Select(p => notificationDtoFactory.CreateLowStockNotification(p, userIds))
            .ToList();

            var createdNotifications = await _userNotificationsService.CreateNotifications(createDtos);

            var group = new LowStockNotificationGroup
            {
                Notifications = createdNotifications.ToList(),
            };

            if (group.Notifications.Any())
                await _notificationService.NotifyLowStockProducts(group);


        }


    }

}
