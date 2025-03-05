namespace BuildingBlocks.Exceptions;

public class DuplicateEmailException : Exception
{
    public DuplicateEmailException(string email)
        : base($"A contact with the same Email '{email}' already exists.")
    { }
}