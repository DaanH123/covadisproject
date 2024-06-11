using covadis.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace covadis.Api
{
    public class DbContextCovadis(DbContextOptions<DbContextCovadis> options)
      : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Trip> Trips { get; set; }
    }
}
