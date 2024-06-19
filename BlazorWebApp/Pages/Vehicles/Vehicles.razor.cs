using BlazorWebApp.State;
using covadis.Shared.Constants;
using covadis.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
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

        protected override async Task OnInitializedAsync()
        {
            vehicles = await VehicleHttpClient.GetVehiclesAsync();
        }

        private void AddNewVehicle()
        {
            NavigationManager.NavigateTo("/vehicle/create");
        }
    }
}
