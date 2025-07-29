using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.SalesInvoice.Commands.Create;
using Project.Application.Features.SalesInvoice.Dtos;
using Project.Application.Features.SalesInvoice.Queries.ExportPdf;
using Project.Application.Features.SalesInvoice.Queries.GetAll;
using Project.Application.Features.SalesInvoice.Queries.GetById;
using Shared;
using Shared.Results;

namespace InventorySystem.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{ version:apiVersion}/[controller]")]
    [ApiController]
    public class SalesInvoiceController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// 📄 Get all sales invoices with optional filters and pagination.
        /// Accessible by Admins, Managers, and Salespersons.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Salesperson")]
        public async Task<ActionResult<SuccessWithData<PaginatedResult<SalesInvoiceSummaryDto>>>> GetAllInvoices([FromQuery] GetSalesInvoicesQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(ApiResponseFactory.Success(result));
        }

        /// <summary>
        /// 📑 Get detailed information of a specific sales invoice by ID.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager,Salesperson")]
        public async Task<ActionResult<SuccessWithData<SalesInvoiceDetailsDto>>> GetInvoiceById([FromRoute] int id)
        {
            var result = await mediator.Send(new GetSalesInvoiceByIdQuery(id));
            return Ok(ApiResponseFactory.Success(result));
        }

        /// <summary>
        ///  Create a new sales invoice.
        /// Accessible by Salespersons and Admins.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin,Salesperson")]
        public async Task<ActionResult<SuccessWithData<SalesInvoiceDetailsDto>>> CreateInvoice([FromBody] CreateSalesInvoiceCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(ApiResponseFactory.Success(result));
        }

        /// <summary>
        /// 📤 Export a sales invoice to PDF by ID.
        /// </summary>
        [HttpGet("{id}/pdf")]
        [Authorize(Roles = "Admin,Manager,Salesperson")]
        public async Task<IActionResult> ExportPdf(int id)
        {
            var query = new ExportSalesInvoicePdfQuery(id);
            var pdfBytes = await mediator.Send(query);
            return File(pdfBytes, "application/pdf", $"Sales-Invoice-{id}.pdf");
        }
    }
}
