
using Domain.Entities;
using MediatR;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Common.Validators;
using Project.Application.Features.Inventory.Dtos;
using Project.Application.Features.PurchaseOrders.Dtos;
using Project.Application.Features.PurchaseOrders.Interfaces;

namespace Project.Application.Features.PurchaseOrders.Commands.Update
{
    internal class UpdatePurchaseOrderCommandHandler : IRequestHandler<UpdatePurchaseOrderCommand, PurchaseOrderResultDto>
    {



        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IEntityValidator<PurchaseOrder> _purchaseOrderValidator;
        private readonly IEntityValidator<PurchaseOrderItem> _itemValidator;
        private readonly IEntityValidator<Product> _productValidator;
        private readonly IInventoryService inventoryService;

        public UpdatePurchaseOrderCommandHandler(
            IPurchaseOrderService purchaseOrderService,
            IUnitOfWork unitOfWork,
            IEntityValidator<PurchaseOrder> purchaseOrderValidator,
            IEntityValidator<PurchaseOrderItem> itemValidator,
            IInventoryService inventoryService,
            IEntityValidator<Product> productValidator)
        {
            _purchaseOrderService = purchaseOrderService;
            _unitOfWork = unitOfWork;

            this.inventoryService = inventoryService;
            _purchaseOrderValidator = purchaseOrderValidator;
            _itemValidator = itemValidator;
            _productValidator = productValidator;
        }

        public async Task<PurchaseOrderResultDto> Handle(UpdatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {



            await _purchaseOrderValidator.ValidateExistsAsync(request.Id, "Purchase order");

            // 🛡️ 2. Validate that all requested item IDs exist
            var itemIds = request.Items.Select(i => i.ItemId).ToList();
            await _itemValidator.ValidateExistAsync(itemIds, "Purchase order item");

            // 🛡️ 3. Validate that all product IDs exist
            var productIds = request.Items.Select(i => i.ProductId).ToList();
            await _productValidator.ValidateExistAsync(productIds, "Product");


            await _purchaseOrderService.UpdatePurchaseOrder(request);


            await _unitOfWork.Commit(cancellationToken);


            var order = await _purchaseOrderService.GetPurchaseOrderById(request.Id);

            var x = order.OrderItems.Select(i => new InventoryStockAdjustmentDto
            {
                ProductId = i.ProductId,
                QuantityChange = i.QuantityReceived,
            }).ToList();

            await inventoryService.AdjustStockAsync(x);


            return order;
        }
    }
}
