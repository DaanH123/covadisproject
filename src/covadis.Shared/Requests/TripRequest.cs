﻿namespace covadis.Shared.Requests
{
    public class TripRequest
    {
        public int ReservationId { get; set; }
        public int OdometerStart { get; set; }
        public int OdometerEnd { get; set; }
        public ICollection<AddressRequest>? Addresses { get; set; }
    }

    public class AddressRequest
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
    }
}
