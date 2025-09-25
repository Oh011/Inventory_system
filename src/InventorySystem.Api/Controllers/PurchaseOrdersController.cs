using InventorySystem.Application.Features.Products.Dtos;
using InventorySystem.Application.Features.PurchaseOrders.Commands.Cancel;
using InventorySystem.Application.Features.PurchaseOrders.Commands.Create;
using InventorySystem.Application.Features.PurchaseOrders.Commands.Update;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;
using InventorySystem.Application.Features.PurchaseOrders.Queries.ExportPf;
using InventorySystem.Application.Features.PurchaseOrders.Queries.GetAll;
using InventorySystem.Application.Features.PurchaseOrders.Queries.GetById;
using InventorySystem.Application.Features.PurchaseOrders.Queries.GetProductByBarcodeForPurchase;
using InventorySystem.Application.Features.PurchaseOrders.Queries.PurchaseOrderOverview;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Results;

namespace InventorySystem.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{ version:apiVersion}/[controller]")]
    [ApiController]
    public class PurchaseOrdersController(IMediator mediator) : ControllerBase
    {




        /// <summary>
        /// Get a product by barcode for a given supplier (used in purchase order creation).
        /// </summary>
        [HttpGet("{supplierId}/products/by-barcode/{barcode}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<SuccessWithData<ProductPurchaseOrderLookUpDto>> GetProductByBarcodeForSupplier(int supplierId, string barcode)
        {
            var query = new GetProductByBarcodeForPurchaseQuery(supplierId, barcode);

            var result = await mediator.Send(query);


            return ApiResponseFactory.Success(result);
        }



        /// <summary>
        /// 🔎 Get a paginated list of all purchase orders with filters (supplier, status, date, etc.)
        /// Accessible by Admins, Managers, and Warehouse staff.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Warehouse")]

        public async Task<ActionResult<SuccessWithData<PaginatedResult<PurchaseOrderListDto>>>> GetAllOrders([FromQuery] GetPurchaseOrdersQuery query)
        {

            var result = await mediator.Send(query);


            return Ok(ApiResponseFactory.Success(result));
        }




        /// <summary>
        /// 📄 Export a specific purchase order as a PDF document.
        /// Accessible by Admins, Managers, and Warehouse staff.
        /// </summary>
        [HttpGet("{id}/pdf")]
        //[Authorize(Roles = "Admin,Manager,Warehouse")]
        public async Task<IActionResult> ExportPdf(int id)
        {
            var query = new ExportPurchaseOrderPdfQuery(id);

            var pdfBytes = await mediator.Send(query);

            return File(pdfBytes, "application/pdf", $"purchase-order-{id}.pdf");
        }





        /// <summary>
        /// 🔍 Get full details of a specific purchase order by ID.
        /// Accessible by Admins, Managers, and Warehouse staff.
        /// </summary>
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin,Manager,Warehouse")]


        public async Task<ActionResult<SuccessWithData<PurchaseOrderDetailDto>>> GetOrderById([FromRoute] int id)
        {

            var query = new GetPurchaseOrderByIdQuery(id);


            var result = await mediator.Send(query);

            return Ok(ApiResponseFactory.Success(result));
        }





        /// <summary>
        /// ➕ Create a new purchase order.
        /// Only Admins can perform this action.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<ActionResult<SuccessWithData<PurchaseOrderDetailDto>>> CreatePurchaseOrder([FromBody] CreatePurchaseOrderCommand command)
        {


            var result = await mediator.Send(command);

            return Ok(ApiResponseFactory.Success(result));
        }




        /// <summary>
        /// ❌ Cancel an existing purchase order.
        /// Accessible by Admins and Managers.
        /// </summary>
        [HttpPatch("{id}/cancel")]
        [Authorize(Roles = "Admin,Manager")]


        public async Task<ActionResult<SuccessMessage>> CancelPurchaseOrder([FromRoute] int id, [FromBody] CancelPurchaseOrderRequest request)
        {

            var command = new CancelPurchaseOrderCommand(id, request.RowVersion);

            var result = await mediator.Send(command);


            return Ok(ApiResponseFactory.Success(result));

        }



        /// <summary>
        /// ✅ Update the received quantities of a purchase order.
        /// Only Warehouse staff are allowed to mark items as received.
        /// </summary>
        [HttpPatch("{id}/receive")]


        public async Task<ActionResult<SuccessWithData<PurchaseOrderDetailDto>>> UpdatePurchaseOrder([FromRoute] int id, [FromBody] UpdatePurchaseOrderRequest request)
        {


            var command = new UpdatePurchaseOrderCommand(id, request.RowVersion, request.Items);

            var result = await mediator.Send(command);


            return Ok(ApiResponseFactory.Success(result));

        }


        /// <summary>
        /// 📊 Get overall purchase order summary.
        /// Returns counts of orders by status and total value.
        /// Accessible by Admins, Managers, and Warehouse staff.
        /// </summary>
        [HttpGet("summary")]
        //[Authorize(Roles = "Admin,Manager,Warehouse")]
        public async Task<ActionResult<SuccessWithData<PurchaseOrderOverviewDto>>> GetOverview([FromQuery] GetPurchaseOrderOverviewQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(ApiResponseFactory.Success(result));
        }


    }
}
