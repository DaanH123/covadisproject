using covadis.Api.Context;
using covadis.Api.Models;
using covadis.Shared.Requests;
using covadis.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace covadis.Api.Services
{
    public class TripService
    {
        private readonly DbContextCovadis _context;

        public TripService(DbContextCovadis context)
        {
            _context = context;
        }

        public async Task<TripResponse?> CreateTripAsync(TripRequest request)
        {
            var reservation = await _context.Reservations.FindAsync(request.ReservationId);
            if (reservation == null) return null;

            var trip = new Trip
            {
                ReservationId = request.ReservationId,
                OdometerStart = request.OdometerStart,
                OdometerEnd = request.OdometerEnd,
                Addresses = request.Addresses?.Select(a => new Address
                {
                    Street = a.Street,
                    Number = a.Number,
                    City = a.City,
                    PostalCode = a.PostalCode,
                    Country = a.Country,
                    Longitude = a.Longitude,
                    Latitude = a.Latitude
                }).ToList()
            };

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            return new TripResponse
            {
                Id = trip.Id,
                ReservationId = trip.ReservationId,
                OdometerStart = trip.OdometerStart,
                OdometerEnd = trip.OdometerEnd,
                Addresses = trip.Addresses?.Select(a => new AddressResponse
                {
                    Id = a.Id,
                    Street = a.Street,
                    Number = a.Number,
                    City = a.City,
                    PostalCode = a.PostalCode,
                    Country = a.Country,
                    Longitude = a.Longitude,
                    Latitude = a.Latitude
                }).ToList()
            };
        }

        public async Task<TripResponse?> GetTripByIdAsync(int id)
        {
            var trip = await _context.Trips.Include(t => t.Addresses).FirstOrDefaultAsync(t => t.Id == id);
            if (trip == null) return null;

            return new TripResponse
            {
                Id = trip.Id,
                ReservationId = trip.ReservationId,
                OdometerStart = trip.OdometerStart,
                OdometerEnd = trip.OdometerEnd,
                Addresses = trip.Addresses?.Select(a => new AddressResponse
                {
                    Id = a.Id,
                    Street = a.Street,
                    Number = a.Number,
                    City = a.City,
                    PostalCode = a.PostalCode,
                    Country = a.Country,
                    Longitude = a.Longitude,
                    Latitude = a.Latitude
                }).ToList()
            };
        }

        public async Task<bool> DeleteTripAsync(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null) return false;

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<TripResponse>> GetTripsByReservationIdAsync(int reservationId)
        {
            return await _context.Trips
                .Where(t => t.ReservationId == reservationId)
                .Include(t => t.Addresses)
                .Select(t => new TripResponse
                {
                    Id = t.Id,
                    OdometerStart = t.OdometerStart,
                    OdometerEnd = t.OdometerEnd,
                    Addresses = t.Addresses.Select(a => new AddressResponse
                    {
                        Street = a.Street,
                        City = a.City,
                        Country = a.Country,
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}
