using Carter;
using ContactPersistence.Application.Contacts.Commands.UpdateContact;
using ContactPersistence.Application.DTOs;
using Mapster;
using MediatR;

namespace ContactPersistency.API.Endpoints;

public record UpdateContactRequest(ContactDto Contact);

public record UpdateContactResponse(Guid Id);

public class UpdateContact : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/contacts/{id}", async (Guid id, UpdateContactRequest request, ISender sender) =>
        {
            // Mapeia a requisição para o comando UpdateContactCommand
            var command = request.Adapt<UpdateContactCommand>();
            command.Contact.Id = id; // Atribui o ID do contato a ser atualizado

            // Envia o comando através do MediatR
            var result = await sender.Send(command);

            // Mapeia o resultado para a resposta
            var response = result.Adapt<UpdateContactResponse>();

            // Retorna a resposta de sucesso
            return Results.Ok(response);
        })
        .WithName("UpdateContact")
        .Produces<UpdateContactResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Contact")
        .WithDescription("Update an existing contact by ID");
    }
}
