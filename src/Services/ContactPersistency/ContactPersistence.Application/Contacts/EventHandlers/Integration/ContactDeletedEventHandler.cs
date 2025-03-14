using BuildingBlocks.Messaging.Events;
using ContactPersistence.Application.Contacts.Commands.DeleteContact;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ContactPersistence.Application.Contacts.EventHandlers.Integration;

public class ContactDeletedEventHandler
    (ISender sender, ILogger<ContactDeletedEventHandler> logger)
    : IConsumer<DeleteContactEvent>
{
    public async Task Consume(ConsumeContext<DeleteContactEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = MapToDeleteContactCommand(context.Message);

        await sender.Send(command);
    }

    private DeleteContactCommand MapToDeleteContactCommand(DeleteContactEvent message)
    {
        return new DeleteContactCommand(message.ContactId);
    }
}