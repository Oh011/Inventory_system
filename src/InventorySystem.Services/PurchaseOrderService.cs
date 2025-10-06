
using Application.Exceptions;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects.PurchaseOrder;
using InventorySystem.Application.Common.Helpers;
using InventorySystem.Application.Common.Interfaces;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using InventorySystem.Application.Features.Inventory.Dtos;
using InventorySystem.Application.Features.PurchaseOrders.Commands.Create;
using InventorySystem.Application.Features.PurchaseOrders.Commands.Update;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;
using InventorySystem.Application.Features.PurchaseOrders.Interfaces;
using InventorySystem.Application.Features.PurchaseOrders.Queries.GetAll;
using InventorySystem.Application.Features.PurchaseOrders.Specifications;
using InventorySystem.Application.Features.Suppliers.Interfaces;
using Shared.Errors;
using Shared.Results;

namespace InventorySystem.Services
{


    internal class ProductForPurchaseOrderDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Barcode { get; set; } = null!;
        public decimal CostPrice { get; set; }
    }

    internal class PurchaseOrderService(IStockEventService stockEventService, ITransactionManager transactionManager, IInventoryService inventoryService, IUnitOfWork unitOfWork, IDomainEventDispatcher domainEventDispatcher, ISupplierService supplierService) : IPurchaseOrderService
    {
        public async Task<int> CreatePurchaseOrder(CreatePurchaseOrderCommand order)
        {


            var purchaseOrderRepository = unitOfWork.GetRepository<PurchaseOrder, int>();



            var supplier = await supplierService.GetSupplierBrief(order.SupplierId);

            var productsIds = order.Items.Select(order => order.ProductId).ToList();
            var products = await LoadProducts(productsIds);

            var notFoundIds = productsIds.Except(products.Select(p => p.Key)).ToList()
                ;

            if (notFoundIds.Any())
            {
                //var errors = new Dictionary<string, List<string>>();


                var errors = notFoundIds.ToDictionary(
                id => id.ToString(),
                id => new List<ValidationErrorDetail>
                {
                        new ValidationErrorDetail($"Product with ID {id} not found")
                }
                 );



                var exception = new ValidationException(errors, "Some products are Invalid");

                throw exception;
            }



            var purchaseOrder = new PurchaseOrder
            {
                SupplierId = order.SupplierId,

                ExpectedDate = order.ExpectedDate,
                DeliveryFee = order.DeliveryFee,
                Notes = order.Notes,
                CreatedByEmployeeId = order.CreatedByEmployeeId,

            };

            var valueObjects = order.Items
        .Select(i => new PurchaseOrderItemData(i.ProductId, i.QuantityOrdered, products[i.ProductId].CostPrice))
        .ToList();

            decimal total = purchaseOrder.AddItems(valueObjects);






            await purchaseOrderRepository.AddAsync(purchaseOrder);

            await unitOfWork.Commit();


            await EventDispatcherHelper.RaiseAndDispatch(purchaseOrder, domainEventDispatcher, e =>
            e.MarkAsCreated(supplier.Id, supplier.Name, supplier.Email, purchaseOrder.Status));



            return purchaseOrder.Id;


        }

        public async Task<PaginatedResult<PurchaseOrderListDto>> GetAllPurchaseOrders(GetPurchaseOrdersQuery query)
        {

            var repository = unitOfWork.GetRepository<PurchaseOrder, int>();
            var specifications = new PurchaseOrderSpecifications(query);

            var orders = await repository.GetAllWithProjectionSpecifications(specifications);
            var totalCount = await repository.CountAsync(specifications.Criteria);


            return new PaginatedResult<PurchaseOrderListDto>(

                query.PageIndex, query.pageSize, totalCount, orders

                );
        }

        private async Task<Dictionary<int, ProductForPurchaseOrderDto>> LoadProducts(IEnumerable<int> productIds)
        {


            var productsRepository = unitOfWork.GetRepository<Product, int>();
            var products = (await productsRepository.ListAsync(
                        p => productIds.Contains(p.Id),
                        p => new ProductForPurchaseOrderDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Barcode = p.Barcode,
                            CostPrice = p.CostPrice
                        }
                    )).ToDictionary(p => p.Id, p => p);

            return products;

        }






        public async Task UpdatePurchaseOrder(UpdatePurchaseOrderCommand request)
        {


            var repository = unitOfWork.GetRepository<PurchaseOrder, int>();
            var specifications = new PurchaseOrderWithItemsSpecifications(request.Id);

            transactionManager = await unitOfWork.BeginTransaction();



            try
            {

                var order = await repository.FirstOrDefaultAsync(specifications);

                if (order == null)
                    throw new NotFoundException($"Order with Id {request.Id} not found");




                if (!order.CanTransitionTo(PurchaseOrderStatus.Received))
                    throw new BadRequestException("Invalid");



                var orderItemDict = order.Items?.ToDictionary(i => i.Id, i => i);

                var requestItemDict = request.Items.ToDictionary(i => i.ItemId, i => new { productId = i.ProductId, receivedQuantity = i.QuantityReceived });



                order.ReceiveItems(
                     request.Items.ToDictionary(i => i.ItemId, i => i.QuantityReceived)
                 );


                order?.UpdateStatusBasedOnReceivedQuantities();



                repository.Update(order);



                var x = request.Items.Select(i => new InventoryStockAdjustmentDto
                {
                    ProductId = i.ProductId,
                    QuantityChange = i.QuantityReceived,
                }).ToList();




                var productIds = await inventoryService.AdjustStockAsync(x, transactionManager);

                await unitOfWork.Commit();
                await transactionManager.CommitTransaction();


                await stockEventService.RaiseStockDecreasedEventAsync(productIds);


                var productsDict = requestItemDict.ToDictionary(i => i.Value.productId, i => i.Value.receivedQuantity);



                var supplier = await supplierService.GetSupplierBrief(order.SupplierId);




                await EventDispatcherHelper.RaiseAndDispatch(order, domainEventDispatcher,
                      o => o.MarkAsReceived(supplier.Id, supplier.Name, supplier.Email, order.Status, productsDict));



            }



            catch (Exception ex)
            {

                await transactionManager.RollBackTransaction();
                throw;
            }




        }

        public async Task<PurchaseOrderDetailDto> GetPurchaseOrderById(int id)
        {


            var repository = unitOfWork.GetRepository<PurchaseOrder, int>();


            var specifications = new PurchaseOrderDetailsSpecifications(id);


            var result = await repository.GetByIdWithProjectionSpecifications(specifications);


            if (result == null)
                throw new NotFoundException("order not found");

            return result;

        }
    }
}
