using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TenetTest.Application.Common.Behaviors;

namespace TenetTest.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));

            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            options.AddOpenBehavior(typeof(RequestLoggingBehavior<,>));
            options.AddOpenBehavior(typeof(RequestExceptionHandlerBehavior<,>));
        });

        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

        return services;
    }
}