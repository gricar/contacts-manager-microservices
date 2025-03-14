using BuildingBlocks.CQRS;
using ContactPersistence.Application.DTOs;

namespace ContactPersistence.Application.Contacts.Commands.UpdateContact;

public record UpdateContactCommand(ContactDto Contact) : ICommand<UpdateContactResult>;

public record UpdateContactResult(Guid Id, string Message = "Contact has been successfully updated.");