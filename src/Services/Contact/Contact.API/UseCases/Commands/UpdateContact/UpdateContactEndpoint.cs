using Carter;

namespace Contact.API.UseCases.Commands.CreateContact;

public class CreateContactEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/contacts", async (UpdateContactCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            var response = result.Adapt<UpdateContactResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateContact")
        .Produces<UpdateContactResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Contact")
        .WithDescription("Update Contact");
    }
}
