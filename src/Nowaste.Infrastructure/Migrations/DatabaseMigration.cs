using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nowaste.Infrastructure.DataAccess;

namespace Nowaste.Infrastructure.Migrations;

public class DatabaseMigration {
    public static async Task MigrateAsync(IServiceProvider serviceProvider) {

        var dbContext = serviceProvider.GetRequiredService<NowasteDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
