using covadis.Shared.Clients;
using covadis.Shared.Requests;
using covadis.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Pages.Users
{
    [Authorize]
    [Route("/user/create")]
    public partial class CreateReservation
    {
        private bool isLoading = true;
        private CreateUserRequest Request = new();

        private IEnumerable<VehicleResponse> Vehicles { get; set; }
        private IEnumerable<string> Errors { get; set; } = [];

        [Inject]
        private UserHttpClient userHttpClient { get; set; }

        [Inject]
        private VehicleHttpClient VehicleHttpClient { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private async Task CreateUserAsync()
        {
            var response = await userHttpClient.CreateUserAsync(Request);
            NavigationManager.NavigateTo("/");
        }
    }
}
