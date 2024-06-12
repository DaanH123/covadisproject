using covadis.Api.Context;
using covadis.Shared.Responses;
namespace covadis.Api.Services
{
    public class VehicleService(DbContextCovadis dbContext)
    {
        public IEnumerable<VehicleResponse> GetVehicles()
        {
            return dbContext.Vehicles.Select(x => new VehicleResponse
            {
                Id = x.Id,
                Brand = x.Brand,
                Model = x.Model
            }).ToList();
        }
    }
}
