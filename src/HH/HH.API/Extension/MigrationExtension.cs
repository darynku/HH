using HH.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HH.API.Extension
{
    public static class MigrationExtension
    {
        public static async Task ApplyMigration(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
