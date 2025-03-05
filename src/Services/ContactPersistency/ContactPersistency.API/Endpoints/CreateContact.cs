using Carter;
using ContactPersistence.Application.Contacts.Commands.CreateContact;
using ContactPersistence.Application.DTOs;
using Mapster;
using MediatR;

namespace ContactPersistency.API.Endpoints;

public record CreateContactRequest(ContactDto Contact);

public record CreateContactResponse(Guid Id);

public class CreateContact : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/contacts", async (CreateContactRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateContactCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateContactResponse>();

            return Results.Created($"/contacts/{response.Id}", response);
        })
        .WithName("CreateContact")
        .Produces<CreateContactResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Contact")
        .WithDescription("Create Contact");
    }
}
