
using Domain.Entities;
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using MediatR;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Common.Validators;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;
using InventorySystem.Application.Features.PurchaseOrders.Interfaces;

namespace InventorySystem.Application.Features.PurchaseOrders.Commands.Update
{
    internal class UpdatePurchaseOrderCommandHandler : IRequestHandler<UpdatePurchaseOrderCommand, PurchaseOrderDetailDto>
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

        public async Task<PurchaseOrderDetailDto> Handle(UpdatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {



            await _purchaseOrderValidator.ValidateExistsAsync(request.Id, "Purchase order");

            //  2. Validate that all requested item IDs exist
            var itemIds = request.Items.Select(i => i.ItemId).ToList();
            await _itemValidator.ValidateExistAsync(itemIds, "Purchase order item");

            //  3. Validate that all product IDs exist
            var productIds = request.Items.Select(i => i.ProductId).ToList();
            await _productValidator.ValidateExistAsync(productIds, "Product");


            await _purchaseOrderService.UpdatePurchaseOrder(request);


            await _unitOfWork.Commit(cancellationToken);


            var order = await _purchaseOrderService.GetPurchaseOrderById(request.Id);



            return order;
        }
    }
}
