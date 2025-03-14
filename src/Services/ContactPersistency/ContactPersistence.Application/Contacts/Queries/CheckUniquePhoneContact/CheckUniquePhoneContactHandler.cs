using BuildingBlocks.CQRS;
using ContactPersistence.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactPersistence.Application.Contacts.Queries.CheckUniquePhoneContact;

public class CheckUniquePhoneContactHandler(IApplicationDbContext dbContext)
    : IQueryHandler<CheckUniquePhoneContactQuery, bool>
{
    public async Task<bool> Handle(CheckUniquePhoneContactQuery query, CancellationToken cancellationToken)
    {
        return await dbContext.Contacts.AnyAsync(c => c.Region.DddCode == query.DddCode
                            && c.Phone == query.Phone, cancellationToken);
    }

}