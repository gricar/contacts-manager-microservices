using BuildingBlocks.CQRS;

namespace ContactPersistence.Application.Contacts.Commands.CreateContact;

public record CreateContactCommand(
        string Name,
        int DDDCode,
        string Phone,
        string? Email = null) : ICommand<CreateContactResult>;

public record CreateContactResult(Guid Id);