using BuildingBlocks.Exceptions;
using System.Net;

namespace Contact.API.ValidationServices;

public class ContactValidationService(HttpClient httpClient) : IContactValidationService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _baseUrl = $"{httpClient.BaseAddress}contacts";

    public async Task EnsureContactIsUniqueAsync(string? email, int dddCode, string phone)
    {
        await CheckForUniqueContactAsync(dddCode, phone);

        if (!string.IsNullOrEmpty(email))
        {
            await CheckForUniqueEmailAsync(email);
        }
    }

    public async Task EnsureContactExistsAsync(Guid id)
    {
        await CheckExistingContactAsync(id);
    }

    private async Task CheckExistingContactAsync(Guid id)
    {
        var url = $"{_baseUrl}/{id}";
        var response = await _httpClient.GetAsync(url);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new NotFoundException("Contact", id);
        }

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"An error occurred while checking if the contact exists: {errorMessage}");
        }
    }

    private async Task CheckForUniqueContactAsync(int dddCode, string phone)
    {
        var url = $"{_baseUrl}/exists/phone?dddCode={dddCode}&phone={phone}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"An error occurred while checking if the contact is unique: {errorMessage}");
        }

        var contact = await response.Content.ReadFromJsonAsync<bool>();

        if (contact)
        {
            throw new DuplicateContactException(dddCode, phone);
        }
    }

    private async Task CheckForUniqueEmailAsync(string email)
    {
        var url = $"{_baseUrl}/exists/email?email={email}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"An error occurred while checking if the contact is unique: {errorMessage}");
        }

        var contact = await response.Content.ReadFromJsonAsync<bool>();

        if (contact)
        {
            throw new DuplicateEmailException(email);
        }
    }
}

