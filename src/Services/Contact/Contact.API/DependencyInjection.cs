using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.Messaging.MassTransit;
using Carter;
using Contact.API.ValidationServices;
using FluentValidation;

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

        services.AddHttpClient<IContactValidationService, ContactValidationService>(client =>
        {
            client.BaseAddress = new Uri(configuration["Services:ContactsPersistence"]!);
            client.Timeout = TimeSpan.FromSeconds(10);
        });

        services.AddValidatorsFromAssembly(typeof(Program).Assembly);

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
