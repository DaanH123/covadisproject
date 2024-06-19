using covadis.Api.Context;
using covadis.Api.Models;

namespace covadis.Api.Seeders
{
    public static class VehicleSeeder
    {
        public static void Seed(DbContextCovadis dbContext)
        {
            var existingVehicles = dbContext.Vehicles
                .Select(x => x.LicensePlate)
                .ToList();

            var vehicles = new List<Vehicle>
        {
            new()
            {
                LicensePlate = "AB-12-CD",
                Brand = "Seat",
                Model = "Leon",
                ManufacturedDate = new DateOnly(2018, 6, 1),
                Odometer = 8_675,
            },
        };

            var vehiclesToAdd = vehicles
                .Where(x => !existingVehicles.Contains(x.LicensePlate))
                .ToList();

            dbContext.Vehicles.AddRange(vehiclesToAdd);
            dbContext.SaveChanges();
        }
    }
}
