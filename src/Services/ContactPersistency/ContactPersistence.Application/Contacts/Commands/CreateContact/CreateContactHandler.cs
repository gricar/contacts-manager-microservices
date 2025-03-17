using BuildingBlocks.CQRS;
using ContactPersistence.Application.Data;
using ContactPersistence.Domain.Models;

namespace ContactPersistence.Application.Contacts.Commands.CreateContact;

public class CreateContactHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateContactCommand, CreateContactResult>
{
    public async Task<CreateContactResult> Handle(CreateContactCommand command, CancellationToken cancellationToken)
    {
        var contact = Contact.Create(command.Name, command.DDDCode, command.Phone, command.Email);

        //store in DB
        await dbContext.Contacts.AddAsync(contact, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateContactResult(contact.Id);
    }
}
