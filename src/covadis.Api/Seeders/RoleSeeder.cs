using covadis.Api.Context;
using covadis.Api.Models;

namespace covadis.Api.Seeders
{
    public static class RoleSeeder
    {
        public static void Seed(DbContextCovadis dbContext)
        {
            var existingRoles = dbContext.Roles
                .Select(x => x.Name)
                .ToList();

            var roles = new List<Role>
            {
                new Role { Name = Role.Administrator },
                new Role { Name = Role.Employee }
            };

            var rolesToAdd = roles
                .Where(x => !existingRoles.Contains(x.Name))
                .ToList();

            dbContext.Roles.AddRange(rolesToAdd);
            dbContext.SaveChanges();
        }
    }
}
