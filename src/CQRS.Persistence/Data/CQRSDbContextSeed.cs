using Microsoft.EntityFrameworkCore;

namespace CQRS.Persistence.Data
{
    public class CQRSDbContextSeed
    {
        public static async Task SeedAsync(CQRSDbContext context)
        {       
            context.Database.Migrate();
            await context.SaveChangesSeedAndMigrationDataAsync();
        }


    }
}
