using covadis.Shared.Clients;
using covadis.Shared.Requests;
using covadis.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Pages.Reservation
{
    [Authorize]
    [Route("/reservation/create")]
    public partial class CreateReservation
    {
        private bool isLoading = true;
        private CreateReservationRequest Request = new();

        private IEnumerable<VehicleResponse> Vehicles { get; set; }
        private IEnumerable<string> Errors { get; set; } = [];

        [Inject]
        private ReservationHttpClient ReservationHttpClient { get; set; }

        [Inject]
        private VehicleHttpClient VehicleHttpClient { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Vehicles = await VehicleHttpClient.GetVehiclesAsync();

            Request.VehicleId = 0;
            Request.From = DateTime.Now;
            Request.Until = Request.From.AddDays(1);

            isLoading = false;
        }

        private async Task CreateReservationAsync()
        {
            var response = await ReservationHttpClient.CreateReservationAsync(Request);

            if (response.Errors.Count > 0)
            {
                Errors = response.Errors;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}