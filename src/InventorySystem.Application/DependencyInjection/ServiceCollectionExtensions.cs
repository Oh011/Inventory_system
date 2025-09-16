using Application.Common.Behaviors;
using FluentValidation;
using Infrastructure.Services.Common.Factories;
using InventorySystem.Application.Common.Factories.Interfaces;
using InventorySystem.Application.Common.Validators;
using InventorySystem.Application.Features;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace Application.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {



        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped<INotificationDtoFactory, NotificationDtoFactory>();

            services.AddScoped<IEmailMessageFactory, EmailMessageFactory>();

            services.AddMediatR(T =>
            {

                T.RegisterServicesFromAssembly(typeof(MediatRAssemblyReference).Assembly);
            });

            services.AddValidatorsFromAssembly(typeof(Application.Common.AssemblyReference).Assembly);


            services.AddScoped(typeof(IEntityValidator<>), typeof(EntityValidator<>));


            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            services.AddAutoMapper(typeof(Application.Common.AssemblyReference).Assembly);

            return services;

        }

    }
}
