
using Application.Exceptions;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using MediatR;
using Project.Application.Common.Helpers;
using Project.Application.Common.Interfaces;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Features.Suppliers.Interfaces;

namespace Project.Application.Features.PurchaseOrders.Commands.Cancel
{
    internal class CancelPurchaseOrderCommandHandler : IRequestHandler<CancelPurchaseOrderCommand, string>
    {


        private readonly IUnitOfWork unitOfWork;
        private readonly ISupplierService supplierService;
        private readonly IDomainEventDispatcher domainEventDispatcher;


        public CancelPurchaseOrderCommandHandler(IUnitOfWork unitOfWork, ISupplierService supplierService, IDomainEventDispatcher domainEventDispatcher)

        {
            this.unitOfWork = unitOfWork;
            this.supplierService = supplierService;
            this.domainEventDispatcher = domainEventDispatcher;
        }

        public async Task<string> Handle(CancelPurchaseOrderCommand request, CancellationToken cancellationToken)
        {

            var repository = unitOfWork.GetRepository<PurchaseOrder, int>();

            var order = await repository.GetById(request.Id);


            if (order == null)
                throw new NotFoundException("Purchase Order not found.");



            unitOfWork.EnsureRowVersionMatch(order, request.RowVersion);



            if (!order.CanTransitionTo(PurchaseOrderStatus.Cancelled))
                throw new DomainException("Cannot cancel order in current state.");



            unitOfWork.ApplyRowVersion(order, request.RowVersion);


            order.Status = PurchaseOrderStatus.Cancelled;
            repository.Update(order);


            await unitOfWork.Commit(); // Will publish event and persist


            var supplier = await supplierService.GetSupplierBrief(order.SupplierId);


            await EventDispatcherHelper.RaiseAndDispatch(order, domainEventDispatcher, o => o.MarkAsCanceled(

                   supplier.Id, supplier.Name, supplier.Email, order.Status

                   ), cancellationToken);


            return $" Purchase Order with Id : {request.Id} is canceled ";
        }
    }
}
