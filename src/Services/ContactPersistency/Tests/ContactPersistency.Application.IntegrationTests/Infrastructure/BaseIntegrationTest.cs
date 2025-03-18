using ContactPersistence.Infrastructure.Data;
using ContactPersistency.Application.IntegrationTests.TestData;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContactPersistency.Application.IntegrationTests.Infrastructure;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IAsyncLifetime
{
    private readonly IServiceScope _scope;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();

        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        DataSeeder = new TestDataSeeder(DbContext);

    }

    protected readonly ISender Sender;
    protected readonly ApplicationDbContext DbContext;
    protected readonly TestDataSeeder DataSeeder;


    public Task InitializeAsync()
    {
        return DataSeeder.SeedAsync();
    }

    public Task DisposeAsync()
    {
        return DbContext.Database.ExecuteSqlRawAsync("DELETE FROM Contacts");
    }
}
