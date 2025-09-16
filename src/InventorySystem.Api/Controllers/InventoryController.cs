using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventorySystem.Application.Features.Inventory.Commands.AdjustInventory;
using InventorySystem.Application.Features.Inventory.Dtos;
using InventorySystem.Application.Features.Inventory.Queries.ExportAdjustmentLogsPdf;
using InventorySystem.Application.Features.Inventory.Queries.GetAdjustmentsLogs;
using InventorySystem.Application.Features.Inventory.Queries.GetInventory;
using InventorySystem.Application.Features.Inventory.Queries.GetInventoryValue;
using InventorySystem.Application.Features.Inventory.Queries.GetLowStock;
using Shared;
using Shared.Results;

namespace InventorySystem.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{ version:apiVersion}/[controller]")]
    [ApiController]
    public class InventoryController(IMediator mediator) : ControllerBase
    {



        /// <summary>
        /// View all current stock with optional filters and pagination
        /// </summary>
        [HttpGet]

        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<SuccessWithData<PaginatedResult<InventoryDto>>>> GetInventory([FromQuery] GetInventoryQuery query)
        {
            var result = await mediator.Send(query);

            return Ok(ApiResponseFactory.Success(result));
        }


        /// <summary>
        /// Retrieves a detailed inventory value report including:
        /// - All products with stock, cost, and value
        /// - Total inventory value
        /// - Average cost price
        /// </summary>
        /// <param name="query">Optional filters by name and category</param>
        /// <returns>A detailed inventory valuation report</returns>
        [HttpGet("value-report")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetInventoryValueReport([FromQuery] GetInventoryValueReportQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }




        /// <summary>
        /// ⚠️ Get low stock items (filter by name, category, or critical level)
        /// </summary>
        /// <param name="name">Filter by product name (optional)</param>
        /// <param name="categoryId">Filter by category ID (optional)</param>
        /// <param name="onlyCritical">If true, returns only critically low stock (<= 2)</param>
        /// <param name="sortOptions">Sort by quantity asc/desc</param>
        /// <returns>List of low stock products</returns>


        [HttpGet("low-stock")]
        [Authorize(Roles = "Admin,Manager,Salesperson")]
        public async Task<ActionResult<SuccessWithData<PaginatedResult<LowStockProductDto>>>> GetLowStock([FromQuery] GetLowStockQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(ApiResponseFactory.Success(result));

        }



        /// <summary>
        /// 🛠️ Manually adjust product stock (e.g., damaged, audit correction)
        /// </summary>
        [HttpPost("adjust")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> AdjustStock([FromBody] AdjustInventoryCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(ApiResponseFactory.Success(result));
        }


        /// <summary>
        /// 📄 Get paginated stock adjustment logs with filters and sorting
        /// </summary>
        /// <param name="query">Filters like product, user, date range, quantity change, and sort</param>
        /// <returns>Paginated result of stock adjustment logs</returns>
        [HttpGet("adjustment-logs")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<SuccessWithData<PaginatedResult<StockAdjustmentLogDto>>>> GetAdjustmentLogs([FromQuery] GetAdjustmentLogsQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(ApiResponseFactory.Success(result));
        }


        /// <summary>
        /// 📤 Export stock adjustment logs as a PDF report with optional filters
        /// </summary>
        /// <param name="query">Filter options (product, user, date range, etc.)</param>
        /// <returns>A downloadable PDF file</returns>
        [HttpGet("adjustment-logs/pdf")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> ExportAdjustmentLogsPdf([FromQuery] ExportAdjustmentLogsPdfQuery query)
        {
            var pdfBytes = await mediator.Send(query);

            return File(pdfBytes, "application/pdf", $"StockAdjustmentLogs-{DateTime.UtcNow:yyyyMMddHHmmss}.pdf");
        }


    }
}
