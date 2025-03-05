namespace Contact.API.ValidationServices;

public interface IContactValidationService
{
    Task EnsureContactIsUniqueAsync(string? email, int dddCode, string phone);
}

