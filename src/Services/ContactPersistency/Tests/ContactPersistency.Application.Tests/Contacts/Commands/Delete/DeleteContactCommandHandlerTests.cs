using BuildingBlocks.Exceptions;
using ContactPersistence.Application.Contacts.Commands.DeleteContact;
using ContactPersistence.Application.Data;
using ContactPersistence.Domain.Models;
using ContactPersistency.Application.Tests.Fixtures;
using FluentAssertions;
using Moq;

namespace ContactPersistency.Application.Tests.Contacts.Commands.Delete;

[Collection(nameof(ContactFixtureCollection))]
public class DeleteContactCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _dbMock;
    private readonly DeleteContactHandler _handler;
    private readonly ContactFixture _fixture;

    private Contact _contact;
    private DeleteContactCommand _command;

    public DeleteContactCommandHandlerTests(ContactFixture fixture)
    {
        _dbMock = new Mock<IApplicationDbContext>();
        _handler = new DeleteContactHandler(_dbMock.Object);
        _fixture = fixture;

        _contact = _fixture.CreateValidContact();
        _command = new DeleteContactCommand(_contact.Id);

        _dbMock.Setup(x => x.Contacts.FindAsync(It.IsAny<Guid>())).ReturnsAsync(_contact);
        _dbMock.Setup(x => x.Contacts.Remove(It.IsAny<Contact>()));
        _dbMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
    }

    [Fact(DisplayName = "Should delete a contact successfully when contact exists")]
    [Trait("Category", "Delete Contact - Success")]
    public async Task DeleteContact_ShouldSucceed_WhenContactExists()
    {
        // Act
        var result = await _handler.Handle(_command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<DeleteContactResult>();

        _dbMock.Verify(x => x.Contacts.FindAsync(It.IsAny<Guid>()), Times.Once);
        _dbMock.Verify(x => x.Contacts.Remove(It.IsAny<Contact>()), Times.Once);
        _dbMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Should throw NotFoundException when contact does not exist")]
    [Trait("Category", "Delete Contact - Failure")]
    public async Task DeleteContact_ShouldThrowException_WhenContactDoesNotExists()
    {
        // Arrange
        _dbMock.Setup(x => x.Contacts.FindAsync(It.IsAny<Guid>())).ReturnsAsync((Contact)null!);

        // Act
        Func<Task> act = async () => await _handler.Handle(_command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Entity \"Contact\" ({_contact.Id}) was not found.");
        _dbMock.Verify(x => x.Contacts.FindAsync(It.IsAny<Guid>()), Times.Once);
        _dbMock.Verify(x => x.Contacts.Remove(It.IsAny<Contact>()), Times.Never);
        _dbMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
