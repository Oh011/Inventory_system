using AutoMapper;
using InventorySystem.Dtos;
using InventorySystem.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Common.Parameters;
using Project.Application.Features.Employees.Commands.ActivateEmployee;
using Project.Application.Features.Employees.Commands.CreateEmployee;
using Project.Application.Features.Employees.Commands.UpdateEmployee;
using Project.Application.Features.Employees.Commands.UpdateEmployeeImage;
using Project.Application.Features.Employees.Dtos;
using Project.Application.Features.Employees.Queries.GetEmployees;
using Shared;
using Shared.Results;

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
