using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Toy.EfCore.Learning.Infrastructures.Contexts;

// ReSharper disable TemplateIsNotCompileTimeConstantProblem

namespace Toy.EfCore.Learning.Controllers.Extensions.Extensions;

public static class CustomDbContextExtension
{
    public static void AddLoggerFactoryDbContext<T>(this IServiceCollection services,
        string connectionString) where T : DbContext
    {
        services.AddScoped<T>(serviceProvider =>
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<T>();
            var optionsBuilder = dbContextOptionsBuilder.UseSqlServer(connectionString)
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .ConfigureWarnings(configurationBuilder => configurationBuilder.Log(
                    (RelationalEventId.TransactionStarted, LogLevel.Information),
                    (RelationalEventId.TransactionCommitted, LogLevel.Information),
                    (RelationalEventId.TransactionRolledBack, LogLevel.Information)))
                .UseLoggerFactory(loggerFactory);

            return (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options)!;
        });
    }

    public static void AddLogToDbContext<T>(
        this IServiceCollection services,
        string connectionString) where T : BaseDbContext
    {
        services.AddScoped<T>(_ =>
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<T>();
            var optionsBuilder = dbContextOptionsBuilder.UseSqlServer(connectionString)
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .ConfigureWarnings(configurationBuilder => configurationBuilder.Log(
                    (RelationalEventId.TransactionStarted, LogLevel.Information),
                    (RelationalEventId.TransactionCommitted, LogLevel.Information),
                    (RelationalEventId.TransactionRolledBack, LogLevel.Information)))
                .LogTo(Console.WriteLine, LogLevel.Debug);

            return (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options)!;
        });
    }
}