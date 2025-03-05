using BuildingBlocks.CQRS;
using ContactPersistence.Application.Data;
using ContactPersistence.Application.DTOs;
using ContactPersistence.Domain.Models;

namespace ContactPersistence.Application.Contacts.Commands.CreateContact;

public class CreateContactHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateContactCommand, CreateContactResult>
{
    public async Task<CreateContactResult> Handle(CreateContactCommand command, CancellationToken cancellationToken)
    {
        var contact = CreateNewContact(command.Contact);

        //store in DB
        await dbContext.Contacts.AddAsync(contact, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateContactResult(contact.Id);
    }

    private Contact CreateNewContact(ContactDto contact)
    {
        return Contact.Create(contact.Name, contact.DDDCode, contact.Phone, contact.Email);
    }
}
