using BuildingBlocks.CQRS;

namespace ContactPersistence.Application.Contacts.Queries.CheckUniqueContact;

public record CheckUniqueContactQuery(string? Email, int? DddCode, string? Phone)
    : IQuery<CheckUniqueContactResult>;

public record CheckUniqueContactResult(bool isUnique);
