using BuildingBlocks.CQRS;
using ContactPersistence.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactPersistence.Application.Contacts.Queries.CheckUniqueContact;

public class CheckUniqueContactHandler(IApplicationDbContext dbContext)
    : IQueryHandler<CheckUniqueContactQuery, CheckUniqueContactResult>
{
    public async Task<CheckUniqueContactResult> Handle(CheckUniqueContactQuery query, CancellationToken cancellationToken)
    {
        var contactExists = await dbContext.Contacts.AnyAsync(c => c.Region.DddCode == query.DddCode
                            && c.Phone == query.Phone, cancellationToken);

        var emailExists = await dbContext.Contacts.AnyAsync(c => c.Email == query.Email, cancellationToken);

        return new CheckUniqueContactResult(!(emailExists || contactExists));
    }

}

