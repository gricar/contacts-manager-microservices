using Carter;
using ContactPersistence.Application.Contacts.Queries.CheckUniqueContact;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactPersistency.API.Endpoints;

public class CheckUniqueEmailContact : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/contacts/exists/email", async ([FromQuery] string email, ISender sender) =>
        {
            var response = await sender.Send(new CheckUniqueEmailContactQuery(email));

            return Results.Ok(response);
        })
        .WithName("CheckUniqueContact")
        .Produces<bool>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Check if Contact Email is Unique")
        .WithDescription("Check if Contact Email is Unique");
    }
}