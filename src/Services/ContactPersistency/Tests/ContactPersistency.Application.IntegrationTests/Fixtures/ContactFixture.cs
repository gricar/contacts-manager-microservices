using Bogus;
using ContactPersistence.Application.Contacts.Commands.CreateContact;

namespace ContactPersistency.Application.IntegrationTests.Fixtures;

public class ContactFixture
{
    private readonly Faker _faker;

    public ContactFixture()
    {
        _faker = new Faker();
    }

    public CreateContactCommand CreateValidContactCommand()
    {
        return new CreateContactCommand(
            _faker.Person.FirstName,
            _faker.PickRandom(new[] { 11, 21, 31, 41 }),
            _faker.Phone.PhoneNumber("#########"),
            _faker.Person.Email);
    }

    public CreateContactCommand CreateInvalidContactCommand()
    {
        return new CreateContactCommand(
            _faker.Person.FirstName,
            _faker.PickRandom(new[] { 11, 21, 31, 41 }),
            _faker.Phone.PhoneNumber("###"),
            _faker.Person.Email);
    }
}
