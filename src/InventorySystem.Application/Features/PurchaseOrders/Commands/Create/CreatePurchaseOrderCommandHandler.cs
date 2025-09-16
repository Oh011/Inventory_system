using AutoMapper;
using Domain.Entities;
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using MediatR;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;
using InventorySystem.Application.Features.PurchaseOrders.Interfaces;

namespace InventorySystem.Application.Features.PurchaseOrders.Commands.Create
{
    internal class CreatePurchaseOrderCommandHandler : IRequestHandler<CreatePurchaseOrderCommand, PurchaseOrderDetailDto>
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly ICurrentUserService currentUserService;
        private readonly IMapper _mapper;


        public CreatePurchaseOrderCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper, IPurchaseOrderService purchaseOrderService)
        {

            _unitOfWork = unitOfWork;
            this.currentUserService = currentUserService;
            this.purchaseOrderService = purchaseOrderService;
            this._mapper = mapper;
        }
        public async Task<PurchaseOrderDetailDto> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {


            var employeeRepository = _unitOfWork.GetRepository<Employee, int>();
            var currentUserId = currentUserService.UserId;
            var employeeId = await employeeRepository.FirstOrDefaultAsync(e => e.ApplicationUserId == currentUserId,
                e => e.Id)
                ;

            request.CreatedByEmployeeId = employeeId;



            var orderId = await purchaseOrderService.CreatePurchaseOrder(request);




            var order = await purchaseOrderService.GetPurchaseOrderById(orderId);



            return order;

        }
    }
}
