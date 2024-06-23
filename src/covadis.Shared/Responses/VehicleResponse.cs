namespace covadis.Shared.Responses
{
    public class VehicleResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public int Odometer { get; set; }
        public DateOnly ManufacturedDate { get; set; }
    }
}
