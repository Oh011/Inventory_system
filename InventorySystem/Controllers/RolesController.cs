using Application.Features.roles.Dtos;
using Application.Features.roles.Queries.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    }
}
