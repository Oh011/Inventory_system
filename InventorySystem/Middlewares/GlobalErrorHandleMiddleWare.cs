using Application.Exceptions;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.Net;

namespace InventorySystem.Middlewares
{
    public class GlobalErrorHandleMiddleWare
    {


        private readonly ILogger<GlobalErrorHandleMiddleWare> _logger;

        private readonly RequestDelegate _next;



        public GlobalErrorHandleMiddleWare(ILogger<GlobalErrorHandleMiddleWare> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {


            try
            {

                await _next(context);
            }


            catch (Exception ex)
            {



                await HandleException(ex, context);
            }
        }




        public async Task HandleException(Exception exception, HttpContext context)
        {

            context.Response.ContentType = "application/json";


            //int statusCode = context.Response.StatusCode;






            if (exception is ValidationException e)
            {

                context.Response.StatusCode = 400;



                await context.Response.WriteAsJsonAsync
                (ApiResponseFactory.Failure(e.Message, e.errors, HttpStatusCode.BadRequest));

                return;

            }


            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


            context.Response.StatusCode = exception switch
            {


                BadRequestException => (int)HttpStatusCode.BadRequest,

                NotFoundException => (int)HttpStatusCode.NotFound,


                DomainException => (int)HttpStatusCode.BadRequest,


                DbUpdateConcurrencyException => (int)HttpStatusCode.Conflict,


                ForbiddenException => (int)HttpStatusCode.Forbidden,

                UnAuthorizedException => (int)HttpStatusCode.Unauthorized,

                ConflictException => (int)HttpStatusCode.Conflict,


                _ => (int)HttpStatusCode.InternalServerError,

            };



            var statusCode = (HttpStatusCode)context.Response.StatusCode;


            string message = statusCode switch
            {
                HttpStatusCode.Conflict when exception is DbUpdateConcurrencyException =>
                    "This record was modified by another user. Please reload and try again.",

                HttpStatusCode.InternalServerError =>
                    "An unexpected error occurred. Please try again later.",

                _ => exception.Message
            };


            var response = ApiResponseFactory.Failure(message, statusCode);





            await context.Response.WriteAsJsonAsync(response);
        }



    }




}
