using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Toy.EfCore.Learning.Applications.Services.Databases;

public class DatabaseMigrationService : IDatabaseInitializeService
{
    private readonly ILogger<DatabaseMigrationService> _logger;

    public DatabaseMigrationService(ILogger<DatabaseMigrationService> logger)
    {
        _logger = logger;
    }

    public async Task InitializeAsync(
        IEnumerable<DbContext> dbContexts)
    {
        foreach (var dbContext in dbContexts)
        {
            await MigrateDatabaseAsync(dbContext);
        }
    }

    private async Task MigrateDatabaseAsync<T>(T dbContext) where T : DbContext
    {
        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
        foreach (var pendingMigration in pendingMigrations)
        {
            // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
            _logger.LogDebug(pendingMigration);
        }

        dbContext.Database.EnlistTransaction();
        
        await dbContext.Database.MigrateAsync();
    }
}