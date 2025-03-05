using ContactPersistence.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactPersistence.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Contact> Contacts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
