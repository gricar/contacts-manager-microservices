using Carter;

namespace Contact.API.UseCases.Commands.DeleteContact;

public class DeleteContactEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/contacts/{Id:guid}", async (Guid Id, ISender sender) =>
        {
            var command = new DeleteContactCommand(Id);

            var result = await sender.Send(command);

            return Results.Ok(result);
        })
        .WithName("DeleteContact")
        .Produces<DeleteContactResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Contact")
        .WithDescription("Delete a contact by its ID.");
    }
}