using covadis.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace covadis.Api
{
    public class DbContextCovadis(DbContextOptions<DbContextCovadis> options)
      : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Rit> Ritten { get; set; }
        public DbSet<Auto> Autos { get; set; }
        public DbSet<Registration> Registration { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Werknemer> werknemers { get; set; }
    }
}
