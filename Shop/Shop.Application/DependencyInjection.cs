using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Behaviors;
using System.Reflection;

namespace Shop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
