using BlazorWebApp.Pages.Users;
using BlazorWebApp.State;
using covadis.Shared.Constants;
using covadis.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWebApp.Pages.Vehicles
{
    [Authorize(Roles = Roles.Administrator)]
    [Route("/vehicles")]
    public partial class Vehicles : ComponentBase
    {
        private IEnumerable<VehicleResponse> vehicles { get; set; }

        [Inject]
        private CovadisAuthenticationStateProvider AuthState { get; set; }

        [Inject]
        private VehicleHttpClient VehicleHttpClient { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            vehicles = await VehicleHttpClient.GetVehiclesAsync();
        }

        private void AddNewVehicle()
        {
            NavigationManager.NavigateTo("/vehicle/create");
        }

        private void EditVehicle(int  vehicleId)
        {
            NavigationManager.NavigateTo($"/vehicle/edit/{vehicleId}");
        }

        private async Task DeleteVehicle(int userId)
        {
            var confirmed = await JSRuntime.InvokeAsync<bool>("confirm", new object[] { "Are you sure you want to delete this user?" });
            if (confirmed)
            {
                var result = await VehicleHttpClient.DeleteVehicleAsync(userId);
                if (result)
                {
                    vehicles = await VehicleHttpClient.GetVehiclesAsync();
                }
            }
        }
    }
}
