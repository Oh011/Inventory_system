using Application.Exceptions;
using Application.Features.Auth.Commands.LogIn;
using Application.Features.Auth.Commands.RefreshAccessToken;
using Application.Features.Auth.Commands.RevokeRefreshToken;
using Application.Features.Auth.Dtos;
using InventorySystem.Application.Features.Auth.Commands.PasswordRest;
using InventorySystem.Application.Features.Auth.Commands.ResetPassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace InventorySystem.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]




    public class AuthenticationController : ControllerBase
    {


        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [Authorize]
        [HttpPost("logout")]
        [ProducesResponseType(typeof(FailureMessageOnly), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(SuccessMessage), StatusCodes.Status200OK)]

        public async Task<ActionResult<SuccessMessage>> LogOut([FromBody] LogOutCommand command)
        {



            command.RefreshToken = ExtractRefreshToken(Request);
            var result = await _mediator.Send(command);



            Response.Cookies.Delete("refreshToken");

            return Ok(ApiResponseFactory.Success(result));
        }




        [HttpPost("refresh-access")]
        [ProducesResponseType(typeof(FailureMessageOnly), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(SuccessWithData<LogInUserResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FailureMessageOnly), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SuccessWithData<LogInUserResponseDto>>> RefreshAccessToken([FromBody] RefreshAccessTokenCommand command)
        {



            command.RefreshToken = ExtractRefreshToken(Request);

            var result = await _mediator.Send(command);
            SetCookie(14, result.RefreshToken);

            return Ok(ApiResponseFactory.Success<LogInUserResponseDto>(result.Response));


        }




        [HttpPost("forgot-password")]
        [ProducesResponseType(typeof(SuccessMessage), StatusCodes.Status200OK)]
        public async Task<ActionResult<SuccessMessage>> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {


            var result = await _mediator.Send(command);

            return Ok(ApiResponseFactory.Success(result));
        }


        [HttpPost("reset-password")]

        public async Task<ActionResult<SuccessMessage>> RestPassword([FromBody] RestPasswordCommand command)
        {

            var result = await _mediator.Send(command);


            return Ok(ApiResponseFactory.Success(result));
        }



        /// <summary>
        /// Logs in a user and returns a JWT and refresh token.
        /// </summary>
        /// <param name="request">User login request DTO</param>
        /// <returns>Access token and refresh token</returns>

        [HttpPost("login")]


        public async Task<ActionResult<SuccessWithData<LogInUserResponseDto>>> LogIn([FromBody] LogInUserCommand command)
        {


            var result = await _mediator.Send(command);


            SetCookie(14, result.RefreshToken);


            var response = ApiResponseFactory.Success(result.Response);


            return Ok(response);

        }



        private void SetCookie(int days, string token)
        {

            Response.Cookies.Append("refreshToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Only if you're using HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(days)
            });


        }


        private string ExtractRefreshToken(HttpRequest request)
        {

            var refreshToken = request.Cookies["refreshToken"];




            if (string.IsNullOrEmpty(refreshToken))
            {

                throw new UnAuthorizedException("Refresh token missing.");

            }

            return refreshToken;
        }



    }




}
