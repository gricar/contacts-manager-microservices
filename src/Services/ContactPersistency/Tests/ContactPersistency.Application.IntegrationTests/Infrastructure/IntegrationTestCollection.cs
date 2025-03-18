using ContactPersistency.Application.IntegrationTests.Fixtures;

namespace ContactPersistency.Application.IntegrationTests.Infrastructure;

[CollectionDefinition(nameof(IntegrationTestCollection))]
public class IntegrationTestCollection : ICollectionFixture<IntegrationTestWebAppFactory>, ICollectionFixture<ContactFixture>;
