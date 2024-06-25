using covadis.Shared.Options;
using covadis.Shared.Requests;
using covadis.Shared.Responses;
using Microsoft.Extensions.Options;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace covadis.Shared.Clients
{
    public class ReservationHttpClient
    {
        private readonly HttpClient client;

        public ReservationHttpClient(IHttpClientFactory httpClientFactory, IOptions<ApiOptions> options)
        {
            client = httpClientFactory.CreateClient(nameof(ReservationHttpClient));
            client.BaseAddress = new Uri($"{options.Value.BaseUrl}/reservations");
        }

        public async Task<CreateReservationResponse> CreateReservationAsync(CreateReservationRequest request)
        {
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync(string.Empty, content);

            var responseContent = await response.Content.ReadAsStringAsync();
            var reservation = JsonSerializer.Deserialize<CreateReservationResponse>(responseContent, JsonOptions.SerializerOptions);

            return reservation!;
        }

        public async Task<IEnumerable<ReservationResponse>> GetReservationsAsync()
        {
            var response = await client.GetAsync(string.Empty);

            if (!response.IsSuccessStatusCode)
            {
                return [];
            }

            var content = await response.Content.ReadAsStringAsync();
            var reservations = JsonSerializer.Deserialize<IEnumerable<ReservationResponse>>(content, JsonOptions.SerializerOptions);

            if (reservations is null)
            {
                return [];
            }

            return reservations;
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var response = await client.DeleteAsync($"reservations/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
