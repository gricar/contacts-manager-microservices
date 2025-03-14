using BuildingBlocks.Messaging.Events;
using ContactPersistence.Application.Contacts.Commands.UpdateContact;
using ContactPersistence.Application.DTOs;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ContactPersistence.Application.Contacts.EventHandlers.Integration;

/// <summary>
/// Handler responsável por consumir eventos de atualização de contato (`UpdateContactEvent`) via MassTransit.
/// Ele converte o evento em um comando (`UpdateContactCommand`) e o encaminha ao Mediator para processamento.
/// </summary>
public class ContactUpdatedEventHandler
    (ISender sender, ILogger<ContactUpdatedEventHandler> logger)
    : IConsumer<UpdateContactEvent>
{
    public async Task Consume(ConsumeContext<UpdateContactEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = MapToUpdateContactCommand(context.Message);

        await sender.Send(command);
    }

    private UpdateContactCommand MapToUpdateContactCommand(UpdateContactEvent message)
    {
        var contact = new ContactDto(message.Id, message.Name, message.DDDCode, message.Phone, message.Email);

        return new UpdateContactCommand(contact);
    }
}
