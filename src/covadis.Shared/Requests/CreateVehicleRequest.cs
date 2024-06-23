namespace covadis.Shared.Requests
{
    public class CreateVehicleRequest
    {
        public string LicensePlate { get; set; }
        public int Odometer { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateOnly ManufacturedDate { get; set; }
    }
}
