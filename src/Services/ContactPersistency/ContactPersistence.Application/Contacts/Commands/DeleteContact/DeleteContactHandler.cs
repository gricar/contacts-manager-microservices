using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using ContactPersistence.Application.Data;

namespace ContactPersistence.Application.Contacts.Commands.DeleteContact;

public class DeleteContactHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteContactCommand, DeleteContactResult>
{
    public async Task<DeleteContactResult> Handle(DeleteContactCommand command, CancellationToken cancellationToken)
    {
        // Verifica se o contato existe
        var contact = await dbContext.Contacts.FindAsync(command.Id);

        if (contact == null)
        {
            throw new NotFoundException("Contact", command.Id);
        }

        // Remove o contato do banco de dados
        dbContext.Contacts.Remove(contact);
        await dbContext.SaveChangesAsync(cancellationToken);

        // Retorna o resultado
        return new DeleteContactResult(true);
    }
}