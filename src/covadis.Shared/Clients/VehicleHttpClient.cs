using covadis.Shared.Options;
using covadis.Shared.Requests;
using covadis.Shared.Responses;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class VehicleHttpClient
{
    private readonly HttpClient client;

    public VehicleHttpClient(IHttpClientFactory httpClientFactory, IOptions<ApiOptions> options)
    {
        client = httpClientFactory.CreateClient(nameof(VehicleHttpClient));
        client.BaseAddress = new Uri($"{options.Value.BaseUrl}/vehicles");
    }

    public async Task<IEnumerable<VehicleResponse>> GetVehiclesAsync()
    {
        var response = await client.GetAsync(string.Empty);

        if (!response.IsSuccessStatusCode)
        {
            return Array.Empty<VehicleResponse>();
        }

        var content = await response.Content.ReadAsStringAsync();
        var vehicles = JsonSerializer.Deserialize<IEnumerable<VehicleResponse>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return vehicles ?? Array.Empty<VehicleResponse>();
    }

    public async Task<VehicleResponse> GetVehicleByIdAsync(int id)
    {
        var response = await client.GetAsync($"vehicles/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        var vehicle = JsonSerializer.Deserialize<VehicleResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return vehicle;
    }

    public async Task<VehicleResponse> CreateVehicleAsync(CreateVehicleRequest request)
    {
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(string.Empty, content);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var createdVehicle = JsonSerializer.Deserialize<VehicleResponse>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return createdVehicle;
    }

    public async Task<bool> UpdateVehicleAsync(int id, UpdateVehicleRequest request)
    {
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PutAsync($"vehicles/{id}", content);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteVehicleAsync(int id)
    {
        var response = await client.DeleteAsync($"vehicles/{id}");

        return response.IsSuccessStatusCode;
    }
}
