using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Toy.EfCore.Learning.Exceptions;
using Toy.EfCore.Learning.Infrastructures.Contexts;

namespace Toy.EfCore.Learning.Applications.Services.Databases;

public class DatabaseEnsureCreateService : IDatabaseInitializeService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseEnsureCreateService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task InitializeAsync(IEnumerable<DbContext> dbContexts)
    {
        foreach (var dbContext in dbContexts)
        {
            await EnsureCreatedAsync(dbContext);
        }
    }

    private static async Task EnsureCreatedAsync<T>(T dbContext) where T : DbContext
    {
        var isSuccess = await dbContext.Database.EnsureCreatedAsync();
        if (!isSuccess)
        {
            throw new DatabaseSaveFailException<DatabaseEnsureCreateService>($"{typeof(T).Name}, Ensure create fail");
        }

        var isConnected = await dbContext.Database.CanConnectAsync();
        if (!isConnected)
        {
            throw new DatabaseSaveFailException<DatabaseEnsureCreateService>($"{typeof(T).Name}, Ensure create succeed, but connection is fail");
        }
    }
}