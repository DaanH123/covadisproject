namespace covadis.Shared.Responses
{
    public class ReservationResponse : BaseResponse
    {
        public int Id { get; set; }

        public DateTime From { get; set; }
        public DateTime Until { get; set; }

        public UserResponse User { get; set; }
        public record UserResponse(int Id, string Name);

        public VehicleResponse Vehicle { get; set; }
        public record VehicleResponse(int Id, string Brand, string Model, DateOnly ManufacturerDate);
    }
}
