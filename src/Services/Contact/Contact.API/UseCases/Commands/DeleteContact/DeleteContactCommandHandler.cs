using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;
using Contact.API.ValidationServices;

namespace Contact.API.UseCases.Commands.DeleteContact;

public class DeleteContactCommandHandler(IContactValidationService contactValidationService, IPublishEndpoint publishEndpoint)
    : ICommandHandler<DeleteContactCommand, DeleteContactResponse>
{
    public async Task<DeleteContactResponse> Handle(DeleteContactCommand command, CancellationToken cancellationToken)
    {
        // Verifica se o contato existe antes de tentar excluí-lo
        await contactValidationService.EnsureContactExistsAsync(command.Id);

        // Cria o evento de exclusão
        var eventMessage = new DeleteContactEvent
        {
            ContactId = command.Id
        };

        // Publica o evento
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        // Retorna uma resposta de sucesso
        return new DeleteContactResponse(true);
    }
}