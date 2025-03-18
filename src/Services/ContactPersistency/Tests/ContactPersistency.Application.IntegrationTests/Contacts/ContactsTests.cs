using BuildingBlocks.Exceptions;
using ContactPersistence.Domain.Models;
using ContactPersistency.Application.IntegrationTests.Fixtures;
using ContactPersistency.Application.IntegrationTests.Infrastructure;
using FluentAssertions;

namespace ContactPersistency.Application.IntegrationTests.Contacts;

[Collection(nameof(IntegrationTestCollection))]
public class ContactsTests : BaseIntegrationTest
{
    private readonly ContactFixture _fixture;
    public ContactsTests(IntegrationTestWebAppFactory factory, ContactFixture fixture) : base(factory)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Should create new contact with valid values")]
    [Trait("Category", "Integration")]
    [Trait("Component", "Database")]
    public async Task CreateContactCommand_ShouldAddNewContact_WhenValid()
    {
        //Arrange
        var command = _fixture.CreateValidContactCommand();

        //Act
        var result = await Sender.Send(command);

        //Assert
        var contact = DbContext.Contacts.FirstOrDefault(c => c.Id == result.Id);

        contact.Should().NotBeNull()
            .And.BeOfType<Contact>();
        contact!.Name.Should().NotBeNullOrWhiteSpace()
            .And.Be(command.Name);
        contact.Region.DddCode.Should().BeGreaterThan(0)
            .And.Be(command.DDDCode);
        contact.Phone.Should().HaveLength(9)
            .And.Be(command.Phone);
        contact.Email.Should().NotBeNullOrWhiteSpace()
            .And.Be(command.Email);
    }

    [Fact(DisplayName = "Should throw exception when creating new contact with invalid values")]
    [Trait("Category", "Integration")]
    [Trait("Component", "Database")]
    public async Task CreateContactCommand_ShouldThrowException_WhenPhoneIsInvalid()
    {
        //Arrange
        var command = _fixture.CreateInvalidContactCommand();

        //Act
        Func<Task> act = async () => await Sender.Send(command);

        //Assert
        await act.Should().ThrowAsync<BadRequestException>();
    }
}
