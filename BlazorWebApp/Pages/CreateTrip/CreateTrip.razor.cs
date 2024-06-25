using covadis.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Pages.CreateTrip
{
    [Authorize]
    [Route("/trips/add/{reservationId:int}")]
    public partial class CreateTrip : ComponentBase
    {
        private TripRequest tripRequest = new TripRequest
        {
            Addresses = new List<AddressRequest>()
        };
        private bool isLoading = true;

        [Parameter] public int reservationId { get; set; }

        [Inject] private TripHttpClient TripHttpClient { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            tripRequest.ReservationId = reservationId;
            isLoading = false;
        }

        private async Task HandleValidSubmit()
        {
            var createdTrip = await TripHttpClient.CreateTripAsync(tripRequest);
            if (createdTrip != null)
            {
                NavigationManager.NavigateTo($"/reservations/{createdTrip.Id}/trips");
            }
        }

        private void AddNewAddress()
        {
            tripRequest.Addresses.Add(new AddressRequest());
        }

        private void RemoveAddress(AddressRequest address)
        {
            tripRequest.Addresses.Remove(address);
        }
    }
}
