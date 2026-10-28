using Microsoft.Extensions.DependencyInjection;
using SGCAE.Application.Utilities.Mediator;

namespace SGCAE.Application;


public static class ApplicationServicesRegistry
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IMediator, SimpleMediator>();

      
        var handlers = typeof(ApplicationServicesRegistry).Assembly
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                .Select(i => (Interface: i, Implementation: t)));

        foreach (var handler in handlers)
        {
            services.AddScoped(handler.Interface, handler.Implementation);
        }

        return services;
    }
}