using Application.Features.roles.Commands.CreateRole;
using Application.Features.roles.Dtos;
using Application.Features.roles.Queries.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.roles.Commands.DeleteRole;
using Shared;

namespace InventorySystem.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{ version:apiVersion}/[controller]")]
    [ApiController]
    public class RolesController(IMediator mediator) : ControllerBase
    {


        [HttpGet]

        public async Task<ActionResult<SuccessWithData<IEnumerable<RoleDto>>>> GetRolesAsync()
        {
            var roles = await mediator.Send(new GetRolesQuery());


            return Ok(ApiResponseFactory.Success(roles));
        }


        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteRole([FromRoute] string id)
        {

            await mediator.Send(new DeleteRoleCommand(id));



            return NoContent();

        }


        [HttpPost]

        public async Task<ActionResult<SuccessWithData<RoleDto>>> CreateRole(CreateRoleCommand command)
        {


            var result = await mediator.Send(command);

            return Ok(ApiResponseFactory.Success(result));
        }
    }
}
