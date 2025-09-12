using Infrastructure.Services;
using InventorySystem.ResponseFactories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Project.Application.Common.Interfaces.Services;
using System.Text.Json.Serialization;

namespace InventorySystem.Extensions
{
    public static class PresentationServiceExtensions
    {

        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {



            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    policy
                        .WithOrigins("http://127.0.0.1:5500") // ✅ specify your frontend URL here
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials(); // ✅ allowed only with specific origins
                });
            });


            services.AddControllers()
              .AddJsonOptions(options =>
              {
                  //This tells ASP.NET Core to accept and serialize enum values as strings.

                  options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                  options.JsonSerializerOptions.AllowTrailingCommas = true;
                  options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                  options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;

              });


            services.Configure<ApiBehaviorOptions>(options =>
            {

                options.InvalidModelStateResponseFactory = ValidationResponseFactory.CustomValidationResponse;
            });


            services.AddSignalR();

            services.AddScoped<INotificationService, NotificationService>();

            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true; //--> If a request doesn't specify a version, use the default.
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = new UrlSegmentApiVersionReader(); // use URL path versioning
            });


            services.AddVersionedApiExplorer(options =>
             {
                 options.GroupNameFormat = "'v'VVV"; // Format: v1, v2, etc.
                 options.SubstituteApiVersionInUrl = true;
             });


            return services;


        }

    }



}




