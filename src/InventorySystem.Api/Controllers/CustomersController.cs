using InventorySystem.Api.Dtos.Customers;
using InventorySystem.Application.Features.Customers.Commands.Create;
using InventorySystem.Application.Features.Customers.Commands.Delete;
using InventorySystem.Application.Features.Customers.Commands.Update;
using InventorySystem.Application.Features.Customers.Dtos;
using InventorySystem.Application.Features.Customers.Queries.CustomerLookup;
using InventorySystem.Application.Features.Customers.Queries.GetAllCustomers;
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
        [Authorize(Roles = "Admin,Manager,Salesperson")]

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



        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <param name="request">The updated customer details.</param>
        /// <returns>
        /// A <see cref="CustomerDto"/> wrapped in <see cref="SuccessWithData{T}"/> 
        /// containing the updated customer information.
        /// </returns>
        /// <response code="200">Returns the updated customer data.</response>
        /// <response code="404">If the customer with the specified ID is not found.</response>

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<ActionResult<SuccessWithData<CustomerDto>>> UpdateCustomer([FromRoute] int id, [FromBody] UpdateCustomerRequest request)
        {
            var result = await mediator.Send(new UpdateCustomerCommand(id, request.FullName, request.Phone, request.Email, request.Address));
            return Ok(ApiResponseFactory.Success(result));
        }



        /// <summary>
        /// Soft deletes a customer by marking them as inactive (IsDeleted = true).
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>No content on success.</returns>
        /// <response code="204">If the customer was successfully soft-deleted.</response>
        /// <response code="404">If the customer with the specified ID is not found.</response>

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            await mediator.Send(new DeleteCustomerCommand(id));


            return NoContent();
        }


    }
}
