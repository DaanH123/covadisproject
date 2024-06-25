using covadis.Shared.Options;
using covadis.Shared.Requests;
using covadis.Shared.Responses;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

public class TripHttpClient
{
    private readonly HttpClient _client;

    public TripHttpClient(IHttpClientFactory httpClientFactory, IOptions<ApiOptions> options)
    {
        _client = httpClientFactory.CreateClient(nameof(TripHttpClient));
        _client.BaseAddress = new Uri($"{options.Value.BaseUrl}/trips");
    }

    public async Task<TripResponse?> CreateTripAsync(TripRequest request)
    {
        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await _client.PostAsync(string.Empty, content);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var createdTrip = JsonSerializer.Deserialize<TripResponse>(responseContent, JsonOptions.SerializerOptions);

        return createdTrip;
    }

    public async Task<TripResponse?> GetTripByIdAsync(int id)
    {
        var response = await _client.GetAsync(id.ToString());

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        var trip = JsonSerializer.Deserialize<TripResponse>(content, JsonOptions.SerializerOptions);

        return trip;
    }

    public async Task<bool> DeleteTripAsync(int id)
    {
        var response = await _client.DeleteAsync(id.ToString());

        return response.IsSuccessStatusCode;
    }

    public async Task<List<TripResponse>> GetTripsByReservationIdAsync(int reservationId)
    {
        var response = await _client.GetAsync($"trips/reservations/{reservationId}/trips");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<TripResponse>>();
    }
}
