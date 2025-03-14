using BuildingBlocks.CQRS;
using ContactPersistence.Application.Data;
using ContactPersistence.Application.DTOs;
using ContactPersistence.Domain.Models;

namespace ContactPersistence.Application.Contacts.Commands.UpdateContact;

/// <summary>
/// Handler responsável por processar o comando de atualização de um contato.
/// Ele busca o contato pelo ID, atualiza os dados e os persiste no repositório.
/// Não utiliza bibliotecas do System, garantindo um código mais portátil.
/// </summary>
public class UpdateContactHandler : ICommandHandler<UpdateContactCommand, UpdateContactResult>
{
    private readonly IContactRepository _contactRepository;

    public UpdateContactHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public Task<UpdateContactResult> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        // Buscar contato pelo ID
        var contactTask = _contactRepository.GetByIdAsync(request.Contact.Id);
        if (contactTask == null || contactTask.Result == null)
        {
            return Task.FromResult(new UpdateContactResult(Guid.Empty, "Contact not found."));
        }

        var contact = contactTask.Result;

        // Atualizar os dados do contato
        contact.Name = request.Contact.Name;
        contact.Email = request.Contact.Email;
        contact.PhoneNumber = request.Contact.PhoneNumber;

        // Persistir a atualização no repositório
        var updateTask = _contactRepository.UpdateAsync(contact);

        return Task.FromResult(new UpdateContactResult(contact.Id));
    }
}
