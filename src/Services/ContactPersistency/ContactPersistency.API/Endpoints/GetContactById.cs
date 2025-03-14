using Carter;
using ContactPersistence.Application.Contacts.Queries.CheckContactExists;
using ContactPersistence.Application.DTOs;
using MediatR;

namespace ContactPersistency.API.Endpoints;

public class GetContactById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/contacts/{id:guid}", async (Guid id, ISender sender) =>
        {
            var response = await sender.Send(new GetContactByIdQuery(id));

            return Results.Ok(response);
        })
        .WithName("GetContactById")
        .Produces<ContactDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Contact")
        .WithDescription("Get Contact By Id");
    }
}