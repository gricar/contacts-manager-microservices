using ContactPersistence.Application.Contacts.Commands.CreateContact;
using ContactPersistence.Application.Data;
using ContactPersistence.Domain.Exceptions;
using ContactPersistence.Domain.Models;
using ContactPersistency.Application.Tests.Fixtures;
using FluentAssertions;
using Moq;

namespace ContactPersistency.Application.Tests.Contacts.Commands.Create;

[Collection(nameof(ContactFixtureCollection))]
public class CreateContactCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _dbMock;
    private readonly CreateContactHandler _handler;
    private readonly ContactFixture _fixture;

    public CreateContactCommandHandlerTests(ContactFixture fixture)
    {
        _dbMock = new Mock<IApplicationDbContext>();
        _handler = new CreateContactHandler(_dbMock.Object);
        _fixture = fixture;

        _dbMock.Setup(x => x.Contacts.AddAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()));
        _dbMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
    }


    [Fact(DisplayName = "Should create a contact successfully when data is valid and unique")]
    [Trait("Category", "Create Contact - Success")]
    public async Task CreateContact_ShouldSucess_WhenDataIsValidAndUnique()
    {
        // Arrange
        var contact = _fixture.CreateValidContact();
        var command = _fixture.CreateContactCommandFromEntity(contact);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CreateContactResult>();
        result.Id.Should().NotBeEmpty();

        _dbMock.Verify(x => x.Contacts.AddAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()), Times.Once);
        _dbMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Should create a contact when email is null")]
    [Trait("Category", "Create Contact - Success")]
    public async void CreateContact_ShouldSucess_WhenEmailIsNull()
    {
        // Arrange
        var command = _fixture.CreateValidContactCommandWithEmailNull();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CreateContactResult>();

        _dbMock.Verify(x => x.Contacts.AddAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()), Times.Once);
    }


    [Fact(DisplayName = "Should fail to create contact when name is invalid")]
    [Trait("Category", "Create Contact - Failure - Invalid Name")]
    public async Task CreateContact_ShouldThrowException_WhenNameIsInvalid()
    {
        // Arrange
        var command = _fixture.CreateContactCommandWithInvalidData();

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidNameException>()
            .WithMessage("Name is required.");

        _dbMock.Verify(x => x.Contacts.AddAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact(DisplayName = "Should fail to create contact when email is invalid")]
    [Trait("Category", "Create Contact - Failure - Invalid Email")]
    public async Task CreateContact_ShouldThrowException_WhenEmailIsInvalid()
    {
        // Arrange
        var invalidEmail = "invalid_email";
        var contact = _fixture.CreateValidContact();
        var command = _fixture.CreateContactCommandWithInvalidData(contact.Name, contact.Region.DddCode, contact.Phone, invalidEmail);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidEmailException>()
            .WithMessage($"Email '{command.Email}' must be a valid format.");

        _dbMock.Verify(x => x.Contacts.AddAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact(DisplayName = "Should fail to create contact when phone is invalid")]
    [Trait("Category", "Create Contact - Failure - Invalid Phone")]
    public async Task CreateContact_ShouldThrowException_WhenPhoneIsInvalid()
    {
        // Arrange
        var contact = _fixture.CreateValidContact();
        var command = _fixture.CreateContactCommandWithInvalidData(contact.Name);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidPhoneNumberException>()
            .WithMessage($"Phone '{command.Phone}' must be a 9-digit number.");

        _dbMock.Verify(x => x.Contacts.AddAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
