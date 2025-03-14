using Carter;
using ContactPersistence.Application.Contacts.Queries.CheckUniquePhoneContact;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactPersistency.API.Endpoints;

public class CheckUniquePhoneContact : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/contacts/exists/phone", async (
            [FromQuery] int dddCode,
            [FromQuery] string phone,
            ISender sender) =>
        {
            var response = await sender.Send(new CheckUniquePhoneContactQuery(dddCode, phone));

            return Results.Ok(response);
        })
        .WithName("CheckUniquePhoneContact")
        .Produces<bool>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Check if Contact Phone is Unique")
        .WithDescription("Check if Contact Phone is Unique");
    }
}
