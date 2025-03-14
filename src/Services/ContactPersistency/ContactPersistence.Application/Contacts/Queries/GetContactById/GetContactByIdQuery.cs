using BuildingBlocks.CQRS;
using ContactPersistence.Application.DTOs;

namespace ContactPersistence.Application.Contacts.Queries.CheckContactExists;

public record GetContactByIdQuery(Guid Id) : IQuery<ContactDto>;