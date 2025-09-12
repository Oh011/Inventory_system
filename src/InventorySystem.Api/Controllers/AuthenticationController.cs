using Application.Features.Auth.Commands.LogIn;
using Application.Features.Auth.Commands.RefreshAccessToken;
using Application.Features.Auth.Commands.RevokeRefreshToken;
using Application.Features.Auth.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Auth.Commands.PasswordRest;
using Project.Application.Features.Auth.Commands.ResetPassword;
using Shared;
using System.Net;
using System.Web;

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



        public async Task<ActionResult<SuccessMessage>> LogOut([FromBody] LogOutCommand command)
        {


            var refreshToken = HttpUtility.UrlDecode(Request.Cookies["refreshToken"]);
            command.RefreshToken = refreshToken;



            if (string.IsNullOrEmpty(refreshToken))
            {
                // Either already logged out or cookie missing
                return BadRequest(ApiResponseFactory.Failure("Refresh token not found in cookie.", HttpStatusCode.BadRequest));
            }

            var result = await _mediator.Send(command);



            Response.Cookies.Delete("refreshToken");

            return Ok(ApiResponseFactory.Success(result));
        }


        [HttpPost("refresh-access")]
        public async Task<ActionResult<SuccessWithData<LogInUserResponseDto>>> RefreshAccessToken([FromBody] RefreshAccessTokenCommand command)
        {


            var refreshToken = HttpUtility.UrlDecode(Request.Cookies["refreshToken"]);
            command.RefreshToken = refreshToken;



            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized(ApiResponseFactory.Failure("Refresh token missing.", HttpStatusCode.Unauthorized));
            }

            var result = await _mediator.Send(command);
            SetCookie(14, result.RefreshToken);

            return Ok(ApiResponseFactory.Success<LogInUserResponseDto>(result.Response));


        }




        [HttpPost("forgot-password")]

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



    }




}
