using BuildingBlocks.Messaging.Events;
using ContactPersistence.Application.Contacts.Commands.CreateContact;
using ContactPersistence.Application.DTOs;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ContactPersistence.Application.Contacts.EventHandlers.Integration;

public class ContactCreatedEventHandler
    (ISender sender, ILogger<ContactCreatedEventHandler> logger)
    : IConsumer<CreateContactEvent>
{
    public async Task Consume(ConsumeContext<CreateContactEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = MapToCreateContactCommand(context.Message);

        //var command = context.Message.Adapt<CreateContactCommand>();

        await sender.Send(command);
    }

    private CreateContactCommand MapToCreateContactCommand(CreateContactEvent message)
    {
        var contact = new ContactDto(message.Name, message.DDDCode, message.Phone, message.Email);

        return new CreateContactCommand(contact);
    }
}
