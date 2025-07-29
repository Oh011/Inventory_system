using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Dashboard.Dtos;
using Project.Application.Features.Dashboard.Queries;

namespace InventorySystem.Controllers
{


    [ApiVersion("1.0")]
    [Route("api/v{ version:apiVersion}/[controller]")]
    [Authorize(Roles = "Admin,Manager")] // Optional: restrict access
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }







        /// <summary>
        /// Retrieves the admin dashboard data, including key business metrics.
        /// </summary>
        /// <remarks>
        /// Returns summarized information such as:
        /// - Total products
        /// - Total stock value
        /// - Customer and employee count
        /// - Low stock items
        /// - Today's total sales
        /// - Recent sales invoices & purchase orders
        /// - Latest notifications
        /// 
        /// Accessible by Admin and Manager roles.
        /// </remarks>

        [HttpGet]



        public async Task<ActionResult<DashboardDto>> GetDashboard()
        {
            var result = await _mediator.Send(new GetAdminDashboardQuery());

            return Ok(result);
        }
    }
}


