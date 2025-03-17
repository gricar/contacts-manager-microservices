using BuildingBlocks.Messaging.Events;
using ContactPersistence.Application.Contacts.Commands.CreateContact;
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

        var command = new CreateContactCommand(context.Message.Name, context.Message.DDDCode, context.Message.Phone, context.Message.Email);

        //var command = context.Message.Adapt<CreateContactCommand>();

        await sender.Send(command);
    }
}
