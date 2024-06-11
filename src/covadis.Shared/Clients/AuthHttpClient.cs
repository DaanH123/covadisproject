namespace covadis.Shared.Clients;
using covadis.Shared.Options;
using covadis.Shared.Requests;
using covadis.Shared.Responses;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
public class AuthHttpClient
{
    private readonly HttpClient client;

    public AuthHttpClient(IHttpClientFactory httpClientFactory, IOptions<ApiOptions> options)
    {
        client = httpClientFactory.CreateClient();
        client.BaseAddress = new Uri($"{options.Value.BaseUrl}/auth");
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await client.PostAsync(string.Empty, content);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var authResponse = JsonSerializer.Deserialize<AuthResponse>(responseContent, JsonOptions.SerializerOptions);

        return authResponse;
    }
}
