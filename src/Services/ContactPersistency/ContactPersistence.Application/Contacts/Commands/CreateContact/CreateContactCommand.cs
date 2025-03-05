using BuildingBlocks.CQRS;
using ContactPersistence.Application.DTOs;

namespace ContactPersistence.Application.Contacts.Commands.CreateContact;

public record CreateContactCommand(ContactDto Contact) : ICommand<CreateContactResult>;

public record CreateContactResult(Guid Id);