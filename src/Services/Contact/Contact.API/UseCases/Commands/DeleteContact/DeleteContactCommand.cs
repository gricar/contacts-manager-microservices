using BuildingBlocks.CQRS;

namespace Contact.API.UseCases.Commands.DeleteContact;

public record DeleteContactCommand(
    Guid Id 
) : ICommand<DeleteContactResponse>;