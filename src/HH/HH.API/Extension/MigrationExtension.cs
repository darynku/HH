using HH.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HH.API.Extension
{
    public static class MigrationExtension
    {
        public static async Task ApplyMigration(this WebApplication app)
        {
            var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("Migration");
            logger.LogInformation("Starting migration process...");

            await using var scope = app.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>()
                            ?? throw new Exception("Database context not found. Ensure the database is correctly configured.");

            try
            {
                logger.LogInformation("Attempting to connect to the database...");
                await dbContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred during the migration process.");
                throw;
            }
            logger.LogInformation("Database migration completed successfully.");
        }

    }
}
