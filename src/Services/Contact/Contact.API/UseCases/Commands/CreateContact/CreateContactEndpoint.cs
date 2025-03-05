using Carter;

namespace Contact.API.UseCases.Commands.CreateContact;

public class CreateContactEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/contacts", async (CreateContactCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);

            var response = result.Adapt<CreateContactResponse>();

            return Results.Ok(response);
        })
        .WithName("CreateContact")
        .Produces<CreateContactResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Contact")
        .WithDescription("Create Contact");
    }
}
