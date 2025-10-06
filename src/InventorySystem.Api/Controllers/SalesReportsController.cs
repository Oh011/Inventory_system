using InventorySystem.Api.Responses;
using InventorySystem.Application.Features.Reports.Sales.Dtos;
using InventorySystem.Application.Features.Reports.Sales.Queries.ExportPdf;
using InventorySystem.Application.Features.Reports.Sales.Queries.ExportSalesByCategoryPdf;
using InventorySystem.Application.Features.Reports.Sales.Queries.ExportSalesByCustomerPdf;
using InventorySystem.Application.Features.Reports.Sales.Queries.Sales;
using InventorySystem.Application.Features.Reports.Sales.Queries.SalesByCategoryReport;
using InventorySystem.Application.Features.Reports.Sales.Queries.SalesByCustomerReport;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/reports/sales")]
    [ApiController]
    public class SalesReportsController(IMediator _mediator) : ControllerBase
    {



        [HttpGet("by-product")]
        public async Task<ActionResult<SuccessWithData<IEnumerable<SalesByProductReportDto>>>> GetByProduct([FromQuery] GetSalesByProductReportQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(ApiResponseFactory.Success(result));
        }

        // GET /api/v1/reports/sales/by-product/pdf
        [HttpGet("by-product/pdf")]
        public async Task<IActionResult> GetByProductPdf([FromQuery] ExportSalesByProductReportPdfQuery query)
        {
            var result = await _mediator.Send(query);
            return File(result, "application/pdf", $"SalesByProductReport-{DateTime.Now:yyyyMMdd}.pdf");
        }

        // GET /api/v1/reports/sales/by-category
        [HttpGet("by-category")]
        public async Task<ActionResult<SuccessWithData<IEnumerable<SalesByCategoryReportDto>>>> GetByCategory([FromQuery] GetSalesByCategoryReportQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(ApiResponseFactory.Success(result));
        }

        // GET /api/v1/reports/sales/by-category/pdf
        [HttpGet("by-category/pdf")]
        public async Task<IActionResult> GetByCategoryPdf([FromQuery] ExportSalesByCategoryReportPdfQuery query)
        {
            var result = await _mediator.Send(query);
            return File(result, "application/pdf", $"SalesByCategoryReport-{DateTime.Now:yyyyMMdd}.pdf");
        }

        // GET /api/v1/reports/sales/by-customer
        [HttpGet("by-customer")]
        public async Task<ActionResult<SuccessWithData<IEnumerable<SalesByCustomerReportDto>>>> GetByCustomer([FromQuery] GetSalesByCustomerReportQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(ApiResponseFactory.Success(result));
        }

        // GET /api/v1/reports/sales/by-customer/pdf
        [HttpGet("by-customer/pdf")]
        public async Task<IActionResult> GetByCustomerPdf([FromQuery] ExportSalesByCustomerReportPdfQuery query)
        {
            var result = await _mediator.Send(query);
            return File(result, "application/pdf", $"SalesByCustomerReport-{DateTime.Now:yyyyMMdd}.pdf");
        }

    }
}
