using Carter;
using ContactPersistence.Application.Contacts.Commands.DeleteContact;
using MediatR;

namespace ContactPersistency.API.Endpoints;

// Requisição de delete (não precisa de um DTO, pois só recebe o ID)
public record DeleteContactRequest(Guid Id);

// Resposta de delete
public record DeleteContactResponse(bool Success, string Message = "");

public class DeleteContact : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/contacts/{id:guid}", async (Guid id, ISender sender) =>
        {
            // Cria o comando de delete
            var command = new DeleteContactCommand(id);

            // Envia o comando para o handler
            var result = await sender.Send(command);

            // Retorna a resposta
            return Results.Ok(result);
        })
        .WithName("DeleteContact")
        .Produces<DeleteContactResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Contact")
        .WithDescription("Delete a contact by its ID.");
    }
}