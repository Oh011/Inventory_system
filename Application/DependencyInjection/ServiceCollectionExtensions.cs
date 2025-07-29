using Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Common.Validators;
using Project.Application.Features;


namespace Application.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {



        public static IServiceCollection AddApplication(this IServiceCollection services)
        {


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
