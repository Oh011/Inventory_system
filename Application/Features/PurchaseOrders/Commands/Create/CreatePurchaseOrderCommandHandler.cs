using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Features.PurchaseOrders.Dtos;
using Project.Application.Features.PurchaseOrders.Interfaces;

namespace Project.Application.Features.PurchaseOrders.Commands.Create
{
    internal class CreatePurchaseOrderCommandHandler : IRequestHandler<CreatePurchaseOrderCommand, PurchaseOrderResultDto>
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
        public async Task<PurchaseOrderResultDto> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {


            var employeeRepository = _unitOfWork.GetRepository<Employee, int>();
            var currentUserId = currentUserService.UserId;
            var employeeId = await employeeRepository.FirstOrDefaultAsync(e => e.ApplicationUserId == currentUserId,
                e => e.Id)
                ;

            request.CreatedByEmployeeId = employeeId;



            var orderId = await purchaseOrderService.CreatePurchaseOrder(request);


            await _unitOfWork.Commit();

            var order = await purchaseOrderService.GetPurchaseOrderById(orderId);



            return order;

        }
    }
}
