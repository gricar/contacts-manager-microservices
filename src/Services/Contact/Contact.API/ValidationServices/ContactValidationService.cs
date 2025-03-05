using BuildingBlocks.Exceptions;

namespace Contact.API.ValidationServices;

public class ContactValidationService(HttpClient httpClient) : IContactValidationService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _baseUrl = $"{httpClient.BaseAddress}contacts/exists";

    public async Task EnsureContactIsUniqueAsync(string? email, int dddCode, string phone)
    {
        await CheckForUniqueContactAsync(dddCode, phone);

        await CheckForUniqueEmailAsync(email);
    }

    private async Task CheckForUniqueContactAsync(int dddCode, string phone)
    {
        var url = $"{_baseUrl}?dddCode={dddCode}&phone={phone}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"An error occurred while checking if the contact is unique: {errorMessage}");
        }

        var content = await response.Content.ReadFromJsonAsync<CheckUniqueContactResult>();

        if (content is not null && !content.isUnique)
        {
            throw new DuplicateContactException(dddCode, phone);
        }
    }

    private async Task CheckForUniqueEmailAsync(string? email)
    {
        var url = $"{_baseUrl}?email={email}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"An error occurred while checking if the contact is unique: {errorMessage}");
        }

        var content = await response.Content.ReadFromJsonAsync<CheckUniqueContactResult>();

        if (!string.IsNullOrEmpty(email) && content is not null && !content.isUnique)
        {
            throw new DuplicateEmailException(email);
        }
    }


    public record CheckUniqueContactResult(bool isUnique);
}

