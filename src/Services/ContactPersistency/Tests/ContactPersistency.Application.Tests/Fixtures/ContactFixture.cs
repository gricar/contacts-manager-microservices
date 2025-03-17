using Bogus;
using ContactPersistence.Application.Contacts.Commands.CreateContact;
using ContactPersistence.Domain.Models;

namespace ContactPersistency.Application.Tests.Fixtures;

public class ContactFixture
{
    private readonly Faker _faker;
    private readonly IEnumerable<int> ValidDddCodes;

    public ContactFixture()
    {
        _faker = new Faker();

        ValidDddCodes = new[] { 11, 21, 31, 41 };
    }

    public Contact CreateValidContact()
    {
        return Contact.Create(
            _faker.Person.FirstName,
            _faker.PickRandom(ValidDddCodes),
            _faker.Phone.PhoneNumber("#########"),
            _faker.Person.Email);
    }

    public CreateContactCommand CreateContactCommandFromEntity(Contact contact)
    {
        return new CreateContactCommand(
            contact.Name,
            contact.Region.DddCode,
            contact.Phone,
            contact.Email);
    }

    public CreateContactCommand CreateContactCommandWithInvalidData(string? name = null, int? dddCode = null, string? phone = null, string? email = null)
    {
        return new CreateContactCommand(
            name ?? " ",
            dddCode ?? _faker.PickRandom(ValidDddCodes),
            phone ?? _faker.Phone.PhoneNumber("###"),
            email);
    }

    public CreateContactCommand CreateValidContactCommandWithEmailNull()
    {
        return new CreateContactCommand(
            _faker.Person.FirstName,
            _faker.PickRandom(ValidDddCodes),
            _faker.Phone.PhoneNumber("#########"));
    }
}
