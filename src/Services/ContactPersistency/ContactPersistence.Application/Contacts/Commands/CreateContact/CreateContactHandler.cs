using BuildingBlocks.CQRS;
using ContactPersistence.Application.DTOs;
using ContactPersistence.Domain.Models;

namespace ContactPersistence.Application.Contacts.Commands.CreateContact;

public class CreateContactHandler : ICommandHandler<CreateContactCommand, CreateContactResult>
{
    public async Task<CreateContactResult> Handle(CreateContactCommand command, CancellationToken cancellationToken)
    {
        var contact = CreateNewContact(command.Contact);

        //store in DB

        return new CreateContactResult(contact.Id);
    }

    private Contact CreateNewContact(ContactDto contact)
    {
        return Contact.Create(contact.Name, 11, contact.Phone, contact.Email);
    }
}
