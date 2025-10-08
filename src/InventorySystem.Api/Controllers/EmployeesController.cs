using AutoMapper;
using InventorySystem.Api.Dtos.Employee;
using InventorySystem.Api.Extensions;
using InventorySystem.Api.Responses;
using InventorySystem.Application.Common.Parameters;
using InventorySystem.Application.Features.Employees.Commands.ActivateEmployee;
using InventorySystem.Application.Features.Employees.Commands.CreateEmployee;
using InventorySystem.Application.Features.Employees.Commands.UpdateEmployee;
using InventorySystem.Application.Features.Employees.Commands.UpdateEmployeeImage;
using InventorySystem.Application.Features.Employees.Dtos;
using InventorySystem.Application.Features.Employees.Queries.GetEmployeeProfile;
using InventorySystem.Application.Features.Employees.Queries.GetEmployeeProfileById;
using InventorySystem.Application.Features.Employees.Queries.GetEmployees;
using InventorySystem.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;
using System.Security.Claims;

namespace InventorySystem.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{ version:apiVersion}/[controller]")]
    [ApiController]
    public class EmployeesController(IMediator mediator, IMapper mapper) : ControllerBase
    {




        /// <summary>
        /// Creates a new employee along with an identity user account.
        /// </summary>
        /// <remarks>
        /// Accessible only by Admins. Requires email, username, password, and role.
        /// </remarks>

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<SuccessWithData<CreateEmployeeResponseDto>>> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {

            var result = await mediator.Send(command);


            return Ok(ApiResponseFactory.Success(result));

        }




        /// <summary>
        /// Retrieves the profile details of the currently signed-in employee.
        /// </summary>
        /// <remarks>
        /// This endpoint returns the authenticated user's own profile information based on the access token.
        /// <br/><br/>
        /// <b>Authorization:</b> Requires a valid access token.
        /// </remarks>
        /// <response code="200">Successfully retrieved the employee profile.</response>
        /// <response code="404">Returned if the employee profile could not be found.</response>
        [HttpGet("me")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessWithData<EmployeeProfileDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FailureMessageOnly), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            var result = await mediator.Send(new GetEmployeeProfileQuery(userId!));

            var response = result.ToApiResponse();
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Retrieves the profile details of a specific employee by their ID.
        /// </summary>
        /// <remarks>
        /// This endpoint is accessible only by users with <b>Admin</b> or <b>Manager</b> roles.  
        /// It allows viewing another employee’s profile by specifying their ID.
        /// <br/><br/>
        /// <b>Authorization:</b> Admin or Manager role required.
        /// </remarks>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <response code="200">Successfully retrieved the employee profile.</response>
        /// <response code="404">Returned if no employee exists with the specified ID.</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessWithData<EmployeeProfileDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FailureMessageOnly), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IApiResponse>> GetEmployeeById(int id)
        {
            var result = await mediator.Send(new GetEmployeeProfileByIdQuery(id));

            var response = result.ToApiResponse();
            return StatusCode(response.StatusCode, response);
        }





        /// <summary>
        /// Retrieves a paginated list of all employees with optional filters.
        /// </summary>
        /// <remarks>Only accessible by Admins.</remarks>

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SuccessWithData<PaginatedResult<EmployeeSummaryDto>>>> GetAllEmployees([FromQuery] EmployeeFilterParams filters)
        {
            var query = new GetEmployeesQuery(filters);

            var result = await mediator.Send(query);

            return Ok(ApiResponseFactory.Success(result));
        }





        /// <summary>
        /// Updates an employee's personal and role information.
        /// </summary>
        /// <remarks>Only the logged-in user or Admin can perform this.</remarks>

        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult<SuccessWithData<CreateEmployeeResponseDto>>> UpdateEmployee(int id, [FromBody] UpdateEmployeeRequest request)
        {

            var command = mapper.Map<UpdateEmployeeCommand>(request);
            command.Id = id;

            var result = await mediator.Send(command);


            return Ok(ApiResponseFactory.Success(result));
        }



        /// <summary>
        /// Changes the activation status (active/inactive) of a specific employee.
        /// </summary>
        /// <remarks>Only accessible by Admins.</remarks>
        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/activation-status")]

        public async Task<ActionResult<SuccessMessage>> ChangeEmployeeActivationStatus(int id, [FromQuery] bool IsActive)
        {

            var command = new ChangeEmployeeActivationStatusCommand()
            {
                EmployeeId = id,
                IsActive = IsActive

            };

            var result = await mediator.Send(command);


            return Ok(ApiResponseFactory.Success(result));
        }



        /// <summary>
        /// Uploads or updates the profile image of an employee.
        /// </summary>
        /// <remarks>
        /// Accessible by the employee themselves or an Admin.
        /// This endpoint expects a multipart/form-data request.
        /// </remarks>
        [Authorize]
        [HttpPatch("{id}/profile-image")]

        public async Task<ActionResult<SuccessWithData<string>>> UploadEmployeeImage([FromRoute] int id, [FromForm] UploadEmployeeImageRequest request)
        {

            var imageDto = FileUploadHelper.ToFileUploadDto(request.ImageFile);

            var command = new UpdateEmployeeProfileImageCommand
            {
                EmployeeId = id,
                ProfileImage = imageDto
            };

            var result = await mediator.Send(command);


            return Ok(ApiResponseFactory.Success<string>(result));
        }
    }
}


//StatusCode(response.StatusCode, response) → sets the HTTP status code based on
//Result<T> and returns the IApiResponse object as the JSON body.