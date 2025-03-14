using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;
using Contact.API.ValidationServices;

namespace Contact.API.UseCases.Commands.UpdateContact;

public class UpdateContactCommandHandler(IContactValidationService contactValidationService, IPublishEndpoint publishEndpoint)
    : ICommandHandler<UpdateContactCommand, UpdateContactResponse>
{
    public async Task<UpdateContactResponse> Handle(UpdateContactCommand command, CancellationToken cancellationToken)
    {
        // Valida se o novo contato é único (não pode ter outro contato com o mesmo email, telefone ou DDD)
        await contactValidationService.EnsureContactIsUniqueAsync(command.Email, command.DDDCode, command.Phone);

        // Mapeia o comando para um evento de atualização de contato
        var eventMessage = command.Adapt<UpdateContactEvent>();

        // Publica o evento para notificar outros sistemas
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        // Retorna a resposta de sucesso
        return new UpdateContactResponse(true);
    }
}
