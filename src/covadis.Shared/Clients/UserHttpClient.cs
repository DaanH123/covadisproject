using covadis.Shared.Options;
using covadis.Shared.Requests;
using covadis.Shared.Responses;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class UserHttpClient
{
    private readonly HttpClient _client;

    public UserHttpClient(IHttpClientFactory httpClientFactory, IOptions<ApiOptions> options)
    {
        _client = httpClientFactory.CreateClient(nameof(UserHttpClient));
        _client.BaseAddress = new Uri($"{options.Value.BaseUrl}/users");
    }

    public async Task<UserResponse?> GetUserAsync(int id)
    {
        var response = await _client.GetAsync("users/" + id.ToString());

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
        var response = await _client.GetAsync(string.Empty);

        if (!response.IsSuccessStatusCode)
        {
            return Array.Empty<UserResponse>();
        }

        var content = await response.Content.ReadAsStringAsync();
        var users = JsonSerializer.Deserialize<IEnumerable<UserResponse>>(content, JsonOptions.SerializerOptions);

        return users ?? Array.Empty<UserResponse>();
    }

    public async Task<UserResponse?> CreateUserAsync(CreateUserRequest request)
    {
        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await _client.PostAsync(string.Empty, content);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var createdUser = JsonSerializer.Deserialize<UserResponse>(responseContent, JsonOptions.SerializerOptions);

        return createdUser;
    }

    public async Task<UserResponse?> UpdateUserAsync(int id, UpdateUserRequest request)
    {
        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await _client.PutAsync($"{id}", content);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var updatedUser = JsonSerializer.Deserialize<UserResponse>(responseContent, JsonOptions.SerializerOptions);

        return updatedUser;
    }


    public async Task<bool> DeleteUserAsync(int id)
    {
        var response = await _client.DeleteAsync($"users/{id}");

        return response.IsSuccessStatusCode;
    }
}
