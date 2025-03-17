using BuildingBlocks.CQRS;

namespace ContactPersistence.Application.Contacts.Commands.DeleteContact;

public record DeleteContactCommand(Guid Id) : ICommand<DeleteContactResult>;

public record DeleteContactResult(bool Success, string Message = "Contato deletado com sucesso!!!");