using Application.Common.Interfaces;
using Application.Features.Auth.Interfaces;
using Application.Features.roles.Interfaces;
using Application.Features.Users.Interfaces;
using Domain.Events.Products;
using Domain.Events.PurchaseOrder;
using Hangfire;
using Infrastructure.Events;
using Infrastructure.Identity;
using Infrastructure.Identity.DataSeeding;
using Infrastructure.Identity.Services;
using Infrastructure.Notifications.Orchestrators;
using Infrastructure.Notifications.Senders;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.DataSeeding;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.Background;
using Infrastructure.Services.Common.Factories;
using Infrastructure.Services.Common.Helpers;
using Infrastructure.Services.PdfGenerators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Project.Application.Common.Factories.Interfaces;
using Project.Application.Common.Interfaces;
using Project.Application.Common.Interfaces.Background;
using Project.Application.Common.Interfaces.PdfGenerators;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Common.Notifications.Interfaces;
using Project.Application.Common.Notifications.Senders;
using Project.Application.Features.Products.Intrefaces;
using Sahred.Options;
using Shared.Options;
using System.Security.Claims;
using System.Text;



namespace Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {


        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            // Infrastructure.DependencyInjection.ServiceRegistration.cs
            services.AddScoped<IBackgroundJobService, HangfireBackgroundJobService>();


            services.AddScoped<ITransactionManager, TransactionManager>();
            services.AddScoped<IProductRepository, ProductRepository>();



            services.AddScoped<INotificationSender<PurchaseOrderStatusChangedDomainEvent>, PurchaseOrderStatusChangedNotificationSender>();
            services.AddScoped<INotificationSender<ProductStockDecreasedEvent>, ProductStockDecreasedNotificationSender>();

            services.AddScoped<INotificationSender<PurchaseOrderCanceledDomainEvent>, PurchaseOrderCanceledEmailSender>();

            services.AddScoped<INotificationSender<PurchaseOrderCreatedDomainEvent>, PurchaseOrderCreatedEmailSender>();
            services.AddScoped<INotificationSender<PurchaseOrderReceivedDomainEvent>, PurchaseOrderReceivedEmailSender>();


            services.AddScoped(typeof(INotificationOrchestrator<>), typeof(NotificationOrchestrator<>));
            ;



            services.AddHangfire(config =>
             {
                 config.UseSqlServerStorage(configuration.GetSection("ConnectionStrings")["DefaultConnectionString"]);
             });

            services.AddHangfireServer();

            //app.UseHangfireDashboard(); // optional dashboard




            services.AddScoped<INotificationDtoFactory, NotificationDtoFactory>();

            services.AddScoped<IStockAdjustmentLogPdfGenerator, StockAdjustmentLogPdfGenerator>();


            services.AddScoped<IEmployeeContextService, EmployeeContextService>();

            services.AddScoped<ISalesReportPdfGenerator, SalesReportPdfGenerator>();

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IdentityInitializer, IdentityDbInitializer>();

            services.AddScoped<ISalesInvoicePdfGenerator, SalesInvoicePdfGenerator>();

            services.AddScoped<IConcurrencyHelper, ConcurrencyHelper>();

            services.AddScoped<IPurchaseOrderPdfGenerator, PurchaseOrderPdfGenerator>();



            services.AddScoped<IUploadService, UploadService>();


            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUserService, UserService>();


            services.AddScoped<IAuthorizationService, AuthorizationService>();


            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddScoped<IUriService, UriService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IEmailService, MailKitEmailService>();

            services.AddScoped<ILinkBuilder, LinkBuilder>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ISupplierRepository, SupplierRepository>();

            services.AddScoped<ITokenService, TokenService>();


            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            services.AddDbContext<ApplicationDbContext>(options =>


            options.UseSqlServer(configuration.GetSection("ConnectionStrings")["DefaultConnectionString"]));


            services.AddScoped<IAuthenticationService, AuthenticationService>();

            ConfigureIdentity(services);




            services.Configure<JwtOptions>(configuration.GetSection("jwtOptions"));


            ConfigureJwtOptions(services, configuration);

            services.Configure<SmtpOptions>(configuration.GetSection("SmtpOptions"));

            services.Configure<LinkOption>(configuration.GetSection("LinkOptions"));


            return services;
        }



        private static void ConfigureJwtOptions(this IServiceCollection services, IConfiguration configuration)
        {

            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();


            services.AddAuthentication(options =>
            {

                //{How to check if a user is logged in}
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                //{How to respond to unauthenticated requests}
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }

                ).AddJwtBearer(options =>
                {




                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            // prevent redirect to login page
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = System.Text.Json.JsonSerializer.Serialize(new { message = "Unauthorized" });
                            return context.Response.WriteAsync(result);
                        },


                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our SignalR hub
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                path.StartsWithSegments("/hubs/notifications")) // make sure this matches your actual hub route
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };


                    options.TokenValidationParameters = new TokenValidationParameters
                    {

                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,


                        RoleClaimType = ClaimTypes.Role,


                        ValidAudience = jwtOptions.Audiance,

                        ValidIssuer = jwtOptions.Issuer,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                        ClockSkew = TimeSpan.Zero,
                    };
                }





                );
        }


        private static void ConfigureIdentity(this IServiceCollection services)
        {



            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;

                options.Tokens.EmailConfirmationTokenProvider = "Default";



                // Lockout settings (optional for security)
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;

                // Sign-in settings




            }).AddEntityFrameworkStores<ApplicationDbContext>()
         .AddDefaultTokenProviders();



        }
    }
}
