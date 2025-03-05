using ContactPersistence.Application;
using ContactPersistence.Infrastructure;
using ContactPersistency.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureApiService(builder.Configuration)
    .ConfigureApplicationService(builder.Configuration)
    .ConfigureInfrastructureService(builder.Configuration);

var app = builder.Build();

app.UseApiService();

app.Run();
