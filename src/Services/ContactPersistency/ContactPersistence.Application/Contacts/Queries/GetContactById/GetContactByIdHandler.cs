using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using ContactPersistence.Application.Data;
using ContactPersistence.Application.DTOs;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ContactPersistence.Application.Contacts.Queries.CheckContactExists;

public class GetContactByIdHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetContactByIdQuery, ContactDto>
{
    public async Task<ContactDto> Handle(GetContactByIdQuery query, CancellationToken cancellationToken)
    {
        var contact = await dbContext.Contacts
            .FirstOrDefaultAsync(c => c.Id == query.Id, cancellationToken);

        if (contact is null)
        {
            throw new NotFoundException("Contact", query.Id);
        }

        return contact.Adapt<ContactDto>();
    }
}