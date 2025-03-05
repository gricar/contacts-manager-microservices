using BuildingBlocks.CQRS;

namespace Contact.API.UseCases.Commands.CreateContact;

public record CreateContactCommand(
        string Name,
        int DDDCode,
        string Phone,
        string? Email = null
        ) : ICommand<CreateContactResponse>;
