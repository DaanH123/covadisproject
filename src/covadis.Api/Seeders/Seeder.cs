using covadis.Api.Context;

namespace covadis.Api.Seeders
{
    public static class Seeder
    {
        public static void Seed(this DbContextCovadis dbContext)
        {
            RoleSeeder.Seed(dbContext);
            UserSeeder.Seed(dbContext);
        }
    }
}
