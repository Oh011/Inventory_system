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




        // GET /employees/me
        [HttpGet("me")]
        [Authorize]

        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            var result = await mediator.Send(new GetEmployeeProfileQuery(userId!));

            var response = result.ToApiResponse();
            return StatusCode(response.StatusCode, response);

        }


        // GET /employees/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager")] // restrict access
        public async Task<IActionResult> GetEmployeeById(int id)
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