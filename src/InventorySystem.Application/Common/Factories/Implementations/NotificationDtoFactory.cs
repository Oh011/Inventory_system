using Domain.Enums;
using Domain.Events.PurchaseOrder;
using InventorySystem.Application.Common.Factories.Interfaces;
using InventorySystem.Application.Features.Inventory.Dtos;
using InventorySystem.Application.Features.Notifications.Dtos;

namespace Infrastructure.Services.Common.Factories
{
    public class NotificationDtoFactory : INotificationDtoFactory
    {
        public CreateUserNotificationDto CreatePurchaseOrderStatusNotification(PurchaseOrderStatusChangedDomainEvent domainEvent, List<string> userIds)
        {
            var message = domainEvent.Status switch
            {
                PurchaseOrderStatus.Pending => $"Purchase order #{domainEvent.PurchaseOrderId} was created by {domainEvent.SupplierName}.",
                PurchaseOrderStatus.PartiallyReceived => $"Purchase order #{domainEvent.PurchaseOrderId} was partially received from {domainEvent.SupplierName}.",
                PurchaseOrderStatus.Received => $"Purchase order #{domainEvent.PurchaseOrderId} was received from {domainEvent.SupplierName}.",
                PurchaseOrderStatus.Cancelled => $"Purchase order #{domainEvent.PurchaseOrderId} was cancelled by {domainEvent.SupplierName}.",
                _ => $"Purchase order #{domainEvent.PurchaseOrderId} status changed to {domainEvent.Status}."
            };

            var type = domainEvent.Status switch
            {
                PurchaseOrderStatus.Pending => NotificationType.PurchaseOrderCreated,
                PurchaseOrderStatus.Received => NotificationType.PurchaseOrderReceived,
                PurchaseOrderStatus.Cancelled => NotificationType.PurchaseOrderCancelled,
                _ => NotificationType.GeneralAnnouncement
            };

            return new CreateUserNotificationDto
            {
                NotificationType = type,
                Message = message,
                Users = userIds
            };
        }

        public CreateUserNotificationDto CreateLowStockNotification(LowStockProductDto product, List<string> userIds)
        {
            var message = $"⚠️ Product '{product.ProductName}' is low in stock. Only {product.QuantityInStock} left (threshold: {product.ReorderThreshold}).";

            return new CreateUserNotificationDto
            {
                NotificationType = NotificationType.InventoryLowStock,
                Message = message,
                Users = userIds
            };
        }
    }

}
