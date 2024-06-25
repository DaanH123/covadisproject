using covadis.Shared.Clients;
using covadis.Shared.Constants;
using covadis.Shared.Requests;
using covadis.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Pages.UpdateReservation
{
    [Route("/reservation/edit/{reservationId}")]
    [Authorize(Roles = Roles.Administrator)]
    public partial class UpdateReservation
    {
        [Parameter]
        public string reservationId { get; set; }

        private ReservationResponse Reservation { get; set; }
        private UpdateReservationRequest Request { get; set; } = new UpdateReservationRequest();

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ReservationHttpClient ReservationHttpClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            int.TryParse(reservationId, out int id);
            Reservation = await ReservationHttpClient.GetReservationIdAsync(id);
            if (Reservation != null)
            {
                Request.From = Reservation.From;
                Request.Until = Reservation.Until;
            }
        }

        private async Task UpdateReservationAsync()
        {
            int.TryParse(reservationId, out int id);
            var updatedReservation = await ReservationHttpClient.UpdateReservationAsync(id, Request);
            if (updatedReservation != null)
            {
                NavigationManager.NavigateTo("/home");
            }
        }
    }
}