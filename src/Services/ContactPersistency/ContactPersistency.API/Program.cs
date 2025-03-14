using ContactPersistence.Application;
using ContactPersistence.Infrastructure;
using ContactPersistence.Infrastructure.Data.Extensions;
using ContactPersistency.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureApiService(builder.Configuration)
    .ConfigureApplicationService(builder.Configuration)
    .ConfigureInfrastructureService(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.InitialiseDatabaseAsync();
    //app.ApplyMigrations();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contact Persistency API");
        c.RoutePrefix = string.Empty; // Redireciona a url / para o Swagger
    });
}

app.UseApiService();

app.Run();
