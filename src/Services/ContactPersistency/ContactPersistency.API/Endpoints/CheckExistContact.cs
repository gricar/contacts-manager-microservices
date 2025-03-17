using Carter;
using ContactPersistence.Application.Contacts.Queries.CheckUniqueContact;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactPersistency.API.Endpoints;

public class CheckExistContact : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/contacts/exists", async (
            [FromQuery] string? email,
            [FromQuery] int? dddCode,
            [FromQuery] string? phone,
            ISender sender) =>
        {
            var response = await sender.Send(new CheckUniqueContactQuery(email, dddCode, phone));

            return Results.Ok(response);
        })
        .WithName("CheckUniqueContact")
        .Produces<CheckUniqueContactResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Check if Contact is Unique")
        .WithDescription("Get Contact");
    }
}