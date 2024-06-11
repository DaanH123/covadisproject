namespace covadis.Shared.Responses
{
    public class ReservationResponse : BaseResponse
    {
        public int Id { get; set; }

        public DateTimeOffset From { get; set; }
        public DateTimeOffset Until { get; set; }

        public UserResponse User { get; set; }
        public record UserResponse(int Id, string Name);

        public VehicleResponse Vehicle { get; set; }
        public record VehicleResponse(int Id, string Brand, string Model, DateOnly ManufacturerDate);
    }
}
