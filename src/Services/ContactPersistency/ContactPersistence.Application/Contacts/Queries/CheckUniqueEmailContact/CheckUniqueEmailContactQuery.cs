using BuildingBlocks.CQRS;

namespace ContactPersistence.Application.Contacts.Queries.CheckUniqueContact;

public record CheckUniqueEmailContactQuery(string Email) : IQuery<bool>;
