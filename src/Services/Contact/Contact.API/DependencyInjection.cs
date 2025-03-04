using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.Messaging.MassTransit;
using Carter;

namespace Contact.API;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApiService
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddMessageBroker(configuration);

        return services;
    }

    public static WebApplication UseApiService(this WebApplication app)
    {
        app.MapCarter();

        app.UseExceptionHandler(options => { });

        return app;
    }
}
