using InventorySystem.Application.Features.PurchaseOrders.Commands.Create;
using InventorySystem.Application.Features.PurchaseOrders.Commands.Update;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;
using InventorySystem.Application.Features.PurchaseOrders.Queries.GetAll;
using Shared.Results;

namespace InventorySystem.Application.Features.PurchaseOrders.Interfaces
{
    public interface IPurchaseOrderService
    {


        Task<int> CreatePurchaseOrder(CreatePurchaseOrderCommand order);


        Task<PurchaseOrderDetailDto> GetPurchaseOrderById(int id);

        Task UpdatePurchaseOrder(UpdatePurchaseOrderCommand request);



        Task<PaginatedResult<PurchaseOrderListDto>> GetAllPurchaseOrders(GetPurchaseOrdersQuery query);
    }
}
