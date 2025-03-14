using BuildingBlocks.CQRS;

namespace ContactPersistence.Application.Contacts.Queries.CheckUniquePhoneContact;

public record CheckUniquePhoneContactQuery(int DddCode, string Phone) : IQuery<bool>;
