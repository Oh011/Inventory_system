using Domain.Entities;
using Domain.Enums;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;
using InventorySystem.Application.Features.PurchaseOrders.Specifications;
using MediatR;
using InventorySystem.Application.Common.Interfaces.Repositories;

namespace InventorySystem.Application.Features.PurchaseOrders.Queries.PurchaseOrderOverview
{
    internal class GetPurchaseOrderOverviewQueryHandler : IRequestHandler<GetPurchaseOrderOverviewQuery, PurchaseOrderOverviewDto>
    {

        private readonly IUnitOfWork unitOfWork;


        public GetPurchaseOrderOverviewQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<PurchaseOrderOverviewDto> Handle(GetPurchaseOrderOverviewQuery request, CancellationToken cancellationToken)
        {

            var repository = unitOfWork.GetRepository<PurchaseOrder, int>();




            var specifications = new PurchaseOrderOverviewSpecifications(request, null);



            var overview = (await repository.GetAllWithGrouping(specifications)).FirstOrDefault();



            return overview ?? new PurchaseOrderOverviewDto();

        }
    }


    internal class PurchaseOrderOverviewProjection
    {
        public PurchaseOrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DeliveryFee { get; set; }
    }
}
