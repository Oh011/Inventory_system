using Application.DependencyInjection;
using Infrastructure.DependencyInjection;
using InventorySystem.Extensions;
using InventorySystem.Middlewares;
using InventorySystem.RealTime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Project.Services.DependecyInjcetion;
using Sahred.Options;
using System.Reflection;
namespace InventorySystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.





            builder.Services.AddPresentationServices();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddServices();


            builder.Services.AddSwaggerGen();







            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddApplication();

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("jwtOptions"));


            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });



            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory System ", Version = "v1" });


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token in the format **Bearer YOUR_TOKEN**"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {} // No scopes
        }
    });







            });


            var app = builder.Build();


            app.UseMiddleware<GlobalErrorHandleMiddleWare>();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapGet("/", () => Results.Redirect("/swagger"));
            }






            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();


            app.UseStaticFiles();


            app.MapHub<NotificationHub>("hubs/notifications");


            app.UseAuthentication();
            app.UseAuthorization();


            await app.SeedDbAsync();
            await app.StartHangFire();

            app.MapControllers();

            app.Run();
        }
    }
}
