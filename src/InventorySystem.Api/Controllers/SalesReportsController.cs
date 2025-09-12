using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Reports.Sales.Dtos;
using Project.Application.Features.Reports.Sales.Queries.ExportPdf;
using Project.Application.Features.Reports.Sales.Queries.Sales;
using Shared;

namespace InventorySystem.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{ version:apiVersion}/[controller]")]
    [ApiController]
    public class SalesReportsController(IMediator _mediator) : ControllerBase
    {




        [HttpGet]
        public async Task<ActionResult<SuccessWithData<IEnumerable<SalesReportDto>>>> GetSalesReport([FromQuery] GetSalesReportQuery query)
        {

            var result = await _mediator.Send(query);


            return Ok(ApiResponseFactory.Success(result));
        }


        [HttpGet("pdf")]
        public async Task<IActionResult> GetSalesReport([FromQuery] ExportSalesReportPdfQuery query)
        {
            var result = await _mediator.Send(query);

            return File(result, "application/pdf", $"SalesReport-{DateTime.Now:yyyyMMdd}.pdf");
        }


    }
}
