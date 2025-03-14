using Contact.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApiService(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contact API");
        c.RoutePrefix = string.Empty; // Redireciona a url / para o Swagger
    });
}

app.UseApiService();

app.Run();
