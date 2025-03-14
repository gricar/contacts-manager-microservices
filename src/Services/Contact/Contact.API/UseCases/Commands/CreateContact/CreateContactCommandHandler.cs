using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;
using Contact.API.ValidationServices;

namespace Contact.API.UseCases.Commands.CreateContact;

public class CreateContactCommandHandler(
    IContactValidationService contactValidationService, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CreateContactCommand, CreateContactResponse>
{
    public async Task<CreateContactResponse> Handle(CreateContactCommand command, CancellationToken cancellationToken)
    {
        await contactValidationService.EnsureContactIsUniqueAsync(command.Email, command.DDDCode, command.Phone);

        var eventMessage = command.Adapt<CreateContactEvent>();

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        return new CreateContactResponse(true);
    }
}
