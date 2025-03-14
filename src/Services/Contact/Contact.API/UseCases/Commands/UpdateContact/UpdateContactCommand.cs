using BuildingBlocks.CQRS;

namespace Contact.API.UseCases.Commands.UpdateContact
{
    public sealed record UpdateContactCommand(
        Guid Id,
        string Name,
        int DDDCode,
        string Phone,
        string? Email = null
        ) : ICommand<UpdateContactResponse>;
}