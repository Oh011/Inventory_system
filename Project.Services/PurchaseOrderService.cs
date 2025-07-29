
using Application.Exceptions;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects.PurchaseOrder;
using Project.Application.Common.Helpers;
using Project.Application.Common.Interfaces;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Features.PurchaseOrders.Commands.Create;
using Project.Application.Features.PurchaseOrders.Commands.Update;
using Project.Application.Features.PurchaseOrders.Dtos;
using Project.Application.Features.PurchaseOrders.Interfaces;
using Project.Application.Features.PurchaseOrders.Queries.GetAll;
using Project.Application.Features.PurchaseOrders.Specifications;
using Project.Application.Features.Suppliers.Interfaces;
using Shared.Results;

namespace Project.Services
{


    internal class ProductForPurchaseOrderDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Barcode { get; set; } = null!;
        public decimal CostPrice { get; set; }
    }

    internal class PurchaseOrderService(IUnitOfWork unitOfWork, IDomainEventDispatcher domainEventDispatcher, ISupplierService supplierService) : IPurchaseOrderService
    {
        public async Task<int> CreatePurchaseOrder(CreatePurchaseOrderCommand order)
        {


            var purchaseOrderRepository = unitOfWork.GetRepository<PurchaseOrder, int>();



            var supplier = await supplierService.GetSupplierBrief(order.SupplierId);

            var productsIds = order.Items.Select(order => order.ProductId).ToList();
            var products = await LoadProducts(productsIds);


            if (products.Count < productsIds.Count)
            {
                throw new NotFoundException("One of the products in the order is not found");
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

        public async Task<PaginatedResult<PurchaseOrderSummaryDto>> GetAllPurchaseOrders(GetPurchaseOrdersQuery query)
        {

            var repository = unitOfWork.GetRepository<PurchaseOrder, int>();
            var specifications = new PurchaseOrderSpecifications(query);

            var orders = await repository.GetAllWithProjectionSpecifications(specifications);
            var totalCount = await repository.CountAsync(specifications.Criteria);


            return new PaginatedResult<PurchaseOrderSummaryDto>(

                query.PageIndex, query.pageSize, totalCount, orders

                );
        }

        private async Task<Dictionary<int, ProductForPurchaseOrderDto>> LoadProducts(IEnumerable<int> productIds)
        {


            //→ Keep LoadProducts(...) inside PurchaseOrderService for now.
            //If other modules start using the same logic, refactor it later into ProductService.

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

            var order = await repository.FirstOrDefaultAsync(specifications);


            if (!order.CanTransitionTo(PurchaseOrderStatus.Received))
                throw new BadRequestException("Invalid");


            var orderItemDict = order.Items.ToDictionary(i => i.Id, i => i);

            var requestItemDict = request.Items.ToDictionary(i => i.ItemId, i => new { productId = i.ProductId, receivedQuantity = i.QuantityReceived });


            unitOfWork.EnsureRowVersionMatch(order, request.RowVersion);

            unitOfWork.ApplyRowVersion(order, request.RowVersion); // 👈 This is correct spot


            int totalQuantity = order?.Items.Sum(i => i.QuantityOrdered) ?? 0;
            int totalReceivedQuantity = request.Items.Sum(i => i.QuantityReceived);


            foreach (var item in orderItemDict)
            {


                if (requestItemDict.TryGetValue(item.Key, out var updatedQuantity))
                {

                    item.Value.UpdateQuantityReceived(updatedQuantity.receivedQuantity);
                }

                else
                {

                    throw new NotFoundException($"Item with ID {item.Key} was not found in this order.");
                }
            }


            order.UpdateStatusBasedOnReceivedQuantities();



            repository.Update(order);


            var productsDict = requestItemDict.ToDictionary(i => i.Value.productId, i => i.Value.receivedQuantity);




            var supplier = await supplierService.GetSupplierBrief(order.SupplierId);



            await EventDispatcherHelper.RaiseAndDispatch(order, domainEventDispatcher,
                  o => o.MarkAsReceived(supplier.Id, supplier.Name, supplier.Email, order.Status, productsDict));



        }

        public async Task<PurchaseOrderResultDto> GetPurchaseOrderById(int id)
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
