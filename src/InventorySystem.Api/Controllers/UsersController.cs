using InventorySystem.Controllers.HelperMethods;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Users.Commands.AssignRole;
using Project.Application.Features.Users.Commands.ChangePassword;
using Project.Application.Features.Users.Dtos;
using Shared;

namespace InventorySystem.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{ version:apiVersion}/[controller]")]
    [ApiController]

    public class UsersController(IMediator mediator) : ControllerBase
    {





        [HttpPost("assign-role")]
        [Authorize]

        public async Task<ActionResult<SuccessMessage>> AssignRole([FromBody] AssignRoleCommand command)
        {

            var result = await mediator.Send(command);


            return Ok(ApiResponseFactory.Success(result));

        }


        [HttpPost("change-password")]

        [Authorize]
        public async Task<ActionResult<SuccessMessage>> ChangePassword([FromBody] ChangePasswordDto dto)
        {

            var userId = AuthHelpers.GetUserId(HttpContext);


            var command = new ChangePasswordCommand
            {
                UserId = userId,
                CurrentPassword = dto.CurrentPassword,
                NewPassword = dto.NewPassword,
            };

            var result = await mediator.Send(command);

            return Ok(ApiResponseFactory.Success(result));
        }




    }
}
