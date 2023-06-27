using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Toy.EfCore.Learning.Infrastructures.Contexts;
using Toy.EfCore.Learning.Test.Commons.Extensions;
using Xunit.Abstractions;

namespace Toy.EfCore.Learning.Test.Commons.TestWorks;

// reference : https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0

public class SeparateApplicationTest : IDisposable
{
    protected readonly SeparateWebApplicationFactory<Program> WebApplicationFactory;

    protected SeparateApplicationTest(
        ITestOutputHelper testOutputHelper, bool enableDbLogging = false)
    {
        WebApplicationFactory = new SeparateWebApplicationFactory<Program>(
            (services) =>
            {
                // services.AddDatabaseInitializeService();
                if (enableDbLogging)
                {
                    services.ReplaceTestDbContextWithCrudDatabaseLogging<SchoolContext>(testOutputHelper);
                    services.ReplaceTestDbContextWithCrudDatabaseLogging<BlogContext>(testOutputHelper);
                }
                else
                {
                    services.ReplaceTestDbContext<SchoolContext>(testOutputHelper);
                    services.ReplaceTestDbContext<BlogContext>(testOutputHelper);
                }
            },
            (loggingBuilder =>
            {
                loggingBuilder.ClearProviders()
                    .AddTestConsoleLogger(testOutputHelper);
            }));
    }

    public void Dispose()
    {
        using var scope = WebApplicationFactory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var contexts = new DbContext[]
        {
            serviceProvider.GetRequiredService<SchoolContext>(),
            serviceProvider.GetRequiredService<BlogContext>(),
        };

        foreach (var dbContext in contexts)
        {
            if (dbContext.Database.CanConnect())
            {
                dbContext.Database.EnsureDeleted();
            }
        }

        WebApplicationFactory.Dispose();
    }
}