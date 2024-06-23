using covadis.Shared.Constants;
using covadis.Shared.Requests;
using covadis.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Pages.UpdateVehicle
{
    [Route("/vehicle/edit/{vehicleId}")]
    [Authorize(Roles = Roles.Administrator)]
    public partial class UpdateVehicle
    {
        [Parameter]
        public string vehicleId { get; set; }

        private VehicleResponse Vehicle { get; set; }
        private UpdateVehicleRequest Request { get; set; } = new UpdateVehicleRequest();

        [Inject]
        private VehicleHttpClient VehicleHttpClient { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            int.TryParse(vehicleId, out int id);
            Vehicle = await VehicleHttpClient.GetVehicleByIdAsync(id);
            if (Vehicle != null)
            {
                Request.Brand = Vehicle.Brand;
                Request.Model = Vehicle.Model;
                Request.LicensePlate = Vehicle.LicensePlate;
                Request.Odometer = Vehicle.Odometer;
                Request.ManufacturedDate = Vehicle.ManufacturedDate;
            }
        }

        private async Task UpdateVehicleAsync()
        {
            int.TryParse(vehicleId, out int id);
            var updatedVehicle = await VehicleHttpClient.UpdateVehicleAsync(id, Request);
            if (updatedVehicle != null)
            {
                NavigationManager.NavigateTo("/vehicles");
            }
        }
    }
}