using Carter;
using ContactPersistence.Application.Contacts.Commands.CreateContact;
using MediatR;

namespace ContactPersistency.API.Endpoints;

public class CreateContact : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/contacts", async (CreateContactCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            return Results.Created($"/contacts/{result.Id}", result);
        })
        .WithName("CreateContact")
        .Produces<CreateContactResult>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Contact")
        .WithDescription("Create Contact");
    }
}
