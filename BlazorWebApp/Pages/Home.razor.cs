namespace BlazorWebApp.Pages
{
    using BlazorWebApp.State;
    using covadis.Shared.Clients;
    using covadis.Shared.Responses;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;

    [Authorize]
    [Route("/")]
    public partial class Home : ComponentBase
    {
        private IEnumerable<ReservationResponse> Reservations { get; set; }

        [Inject]
        private ReservationHttpClient ReservationHttpClient { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private CovadisAuthenticationStateProvider AuthState { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Reservations = await ReservationHttpClient.GetReservationsAsync();
        }

        private void GotoReservation()
        {
            NavigationManager.NavigateTo("/reservation/create");
        }

        private void AddTrip(int reservationId)
        {
            NavigationManager.NavigateTo($"/trips/add/{reservationId}");
        }

        private void SeeTrips(int reservationId)
        {
            NavigationManager.NavigateTo($"/reservations/{reservationId}/trips");
        }

        private async Task deleteReservation(int Id)
        {
            var confirmed = await JSRuntime.InvokeAsync<bool>("confirm", new object[] { "Are you sure you want to delete this reservation?" });
            if (confirmed)
            {
                var result = await ReservationHttpClient.DeleteReservationAsync(Id);
                if (result)
                {
                    Reservations = await ReservationHttpClient.GetReservationsAsync();
                }
            }
        }
    }
}
