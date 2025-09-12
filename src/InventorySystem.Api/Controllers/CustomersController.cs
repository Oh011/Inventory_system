using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Customers.Commands.Create;
using Project.Application.Features.Customers.Dtos;
using Project.Application.Features.Customers.Queries.CustomerLookup;
using Project.Application.Features.Customers.Queries.GetAllCustomers;
using Shared;
using Shared.Results;

namespace InventorySystem.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{ version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomersController(IMediator mediator) : ControllerBase
    {


        /// <summary>
        /// Creates a new customer.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<ActionResult<SuccessWithData<CustomerDto>>> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(ApiResponseFactory.Success(result));
        }


        /// <summary>
        /// Returns a lookup list of customers by search term.
        /// </summary>
        [HttpGet("lookup")]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<ActionResult<SuccessWithData<List<CustomerLookUpDto>>>> Lookup([FromQuery] string search)
        {
            var result = await mediator.Send(new GetCustomerLookupQuery { Search = search });
            return Ok(ApiResponseFactory.Success(result));
        }

        /// <summary>
        /// Returns a paginated list of all customers with optional filters.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]


        public async Task<ActionResult<SuccessWithData<PaginatedResult<CustomerDto>>>> GetCustomers([FromQuery] GetCustomersQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(ApiResponseFactory.Success(result));
        }




    }
}
