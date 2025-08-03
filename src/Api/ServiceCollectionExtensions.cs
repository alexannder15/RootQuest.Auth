using Application.EventHandlers;
using Infrastructure.IntegrationEventHandlers;

namespace Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
            );
        });

        return services;
    }

    public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
    {
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
        //services.AddMediatR(cfg =>
        //    cfg.RegisterServicesFromAssembly(typeof(UserRegisteredDomainEvent).Assembly)
        //);

        services.AddMediatR(c =>
            c.RegisterServicesFromAssembly(typeof(SendWelcomeEmailHandler).Assembly)
                .RegisterServicesFromAssembly(typeof(UserRegisteredDomainEventHandler).Assembly)
        );

        //services.AddMediatR(typeof(Program).Assembly);
        return services;
    }
}
