namespace ContactPersistence.Domain.Exceptions;

public class InvalidIdContactException : Exception
{
    public InvalidIdContactException(Guid Id)
        : base($"A contact with that '{Id}' was not found.")
    {}
}