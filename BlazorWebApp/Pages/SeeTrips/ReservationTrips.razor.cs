using covadis.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Pages.SeeTrips
{
    [Authorize]
    [Route("/reservations/{reservationId:int}/trips")]
    public partial class ReservationTrips
    {
        [Parameter] public int ReservationId { get; set; }
        private List<TripResponse> Trips { get; set; } = new List<TripResponse>();
        private bool isLoading = true;

        [Inject]
        private TripHttpClient TripHttpClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Trips = await TripHttpClient.GetTripsByReservationIdAsync(ReservationId);
            isLoading = false;
        }
    }
}