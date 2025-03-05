using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;

namespace Contact.API.UseCases.Commands.CreateContact;

public class CreateContactCommandHandler(IPublishEndpoint publishEndpoint)
    : ICommandHandler<CreateContactCommand, CreateContactResponse>
{
    public async Task<CreateContactResponse> Handle(CreateContactCommand command, CancellationToken cancellationToken)
    {
        var eventMessage = command.Adapt<CreateContactEvent>();

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        return new CreateContactResponse(true);
    }
}
