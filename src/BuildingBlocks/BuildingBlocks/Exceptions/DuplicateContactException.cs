namespace BuildingBlocks.Exceptions;

public class DuplicateContactException : Exception
{
    public DuplicateContactException(int dddCode, string phone)
        : base($"A contact with the same DDD '{dddCode}' and phone '{phone}' already exists.")
    {
    }
}
