using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using ContactPersistence.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactPersistence.Application.Contacts.Queries.CheckContactExists;

public class CheckContactExistsHandler(IApplicationDbContext dbContext)
    : IQueryHandler<CheckContactExistsQuery, CheckContactExistsResult>
{
    public async Task<CheckContactExistsResult> Handle(CheckContactExistsQuery query, CancellationToken cancellationToken)
    {
        // Verifica se o contato existe
        var contactExists = await dbContext.Contacts.AnyAsync(c => c.Id == query.Id, cancellationToken);

        if (!contactExists)
        {
            throw new NotFoundException("Contato não encontrado.");
        }

        return new CheckContactExistsResult(true);
    }
}