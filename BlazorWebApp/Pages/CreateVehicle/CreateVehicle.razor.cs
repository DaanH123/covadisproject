using covadis.Shared.Constants;
using covadis.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Pages.CreateVehicle
{
    [Authorize(Roles = Roles.Administrator)]
    [Route("/vehicle/create")]
    public partial class CreateVehicle
    {
        private CreateVehicleRequest Request { get; set; } = new CreateVehicleRequest();

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private VehicleHttpClient VehicleHttpClient { get; set; }

        private async Task CreateVehicleAsync()
        {
            var createdVehicle = await VehicleHttpClient.CreateVehicleAsync(Request);
            if (createdVehicle != null)
            {
                NavigationManager.NavigateTo("/vehicles");
            }
        }
    }
}