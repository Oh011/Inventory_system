using Project.Application.Features.PurchaseOrders.Commands.Create;
using Project.Application.Features.PurchaseOrders.Commands.Update;
using Project.Application.Features.PurchaseOrders.Dtos;
using Project.Application.Features.PurchaseOrders.Queries.GetAll;
using Shared.Results;

namespace Project.Application.Features.PurchaseOrders.Interfaces
{
    public interface IPurchaseOrderService
    {


        Task<int> CreatePurchaseOrder(CreatePurchaseOrderCommand order);


        Task<PurchaseOrderResultDto> GetPurchaseOrderById(int id);

        Task UpdatePurchaseOrder(UpdatePurchaseOrderCommand request);



        Task<PaginatedResult<PurchaseOrderSummaryDto>> GetAllPurchaseOrders(GetPurchaseOrdersQuery query);
    }
}
