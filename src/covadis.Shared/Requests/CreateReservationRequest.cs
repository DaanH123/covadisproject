namespace covadis.Shared.Requests
{
    public class CreateReservationRequest
    {
        public int VehicleId { get; set; }

        public DateTime From { get; set; }
        public DateTime Until { get; set; }
    }
}
