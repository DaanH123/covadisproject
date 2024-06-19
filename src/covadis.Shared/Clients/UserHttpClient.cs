using covadis.Shared.Options;
using covadis.Shared.Requests;
using covadis.Shared.Responses;
using Microsoft.Extensions.Options;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

public class UserHttpClient
{
    private readonly HttpClient client;

    public UserHttpClient(IHttpClientFactory httpClientFactory, IOptions<ApiOptions> options)
    {
        client = httpClientFactory.CreateClient(nameof(UserHttpClient));
        client.BaseAddress = new Uri($"{options.Value.BaseUrl}/users");
    }

    public async Task<UserResponse?> GetUserAsync(int id)
    {
        var response = await client.GetAsync(id.ToString());

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<UserResponse>(content, JsonOptions.SerializerOptions);

        return user;
    }

    public async Task<IEnumerable<UserResponse>> GetUsersAsync()
    {
        var response = await client.GetAsync(string.Empty);

        if (!response.IsSuccessStatusCode)
        {
            return [];
        }

        var content = await response.Content.ReadAsStringAsync();
        var users = JsonSerializer.Deserialize<IEnumerable<UserResponse>>(content, JsonOptions.SerializerOptions);

        if (users is null)
        {
            return [];
        }

        return users;
    }

    public async Task<CreateUserRequest> CreateUserAsync(CreateUserRequest request)
    {
        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await client.PostAsync(string.Empty, content);

        var responseContent = await response.Content.ReadAsStringAsync();
        var createdUser = JsonSerializer.Deserialize<CreateUserRequest>(responseContent, JsonOptions.SerializerOptions);

        return createdUser;
    }
}