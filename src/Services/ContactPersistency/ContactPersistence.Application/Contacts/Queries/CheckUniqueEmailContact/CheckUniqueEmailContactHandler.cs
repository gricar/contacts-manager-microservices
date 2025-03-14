using BuildingBlocks.CQRS;
using ContactPersistence.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactPersistence.Application.Contacts.Queries.CheckUniqueContact;

public class CheckUniqueEmailContactHandler(IApplicationDbContext dbContext)
    : IQueryHandler<CheckUniqueEmailContactQuery, bool>
{
    public async Task<bool> Handle(CheckUniqueEmailContactQuery query, CancellationToken cancellationToken)
    {
        return await dbContext.Contacts.AnyAsync(c => c.Email == query.Email, cancellationToken);
    }

}
