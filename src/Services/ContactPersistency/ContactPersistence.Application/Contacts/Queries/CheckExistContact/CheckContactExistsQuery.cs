using BuildingBlocks.CQRS;

namespace ContactPersistence.Application.Contacts.Queries.CheckContactExists;

public record CheckContactExistsQuery(Guid Id) : IQuery<CheckContactExistsResult>;