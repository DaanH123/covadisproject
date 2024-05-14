using covadis.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace covadis.Api
{
    public class DbContextCovadis(DbContextOptions<DbContextCovadis> options)
      : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
