using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using covadis.Shared.Options;
using covadis.Shared.Responses;
using Microsoft.Extensions.Options;

namespace covadis.Shared.Clients
{
    public class RoleHttpClient
    {
        private readonly HttpClient _httpClient;

        public RoleHttpClient(HttpClient httpClient, IOptions<ApiOptions> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
        }

        public async Task<IEnumerable<RoleResponse>> GetRolesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<RoleResponse>>("api/Roles");

                if (response == null)
                {
                    return new List<RoleResponse>();
                }

                return response;
            }
            catch (Exception ex)
            {
                // Handle exception as needed, log or throw further
                Console.WriteLine($"Exception occurred while fetching roles: {ex.Message}");
                return new List<RoleResponse>();
            }
        }
    }
}
