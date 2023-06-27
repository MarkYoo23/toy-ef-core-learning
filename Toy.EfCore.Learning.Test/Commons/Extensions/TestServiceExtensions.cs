using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Toy.EfCore.Learning.Applications.Services.Databases;
using Toy.EfCore.Learning.Test.Commons.Loggers;
using Xunit.Abstractions;

namespace Toy.EfCore.Learning.Test.Commons.Extensions;

public static class TestServiceExtensions
{
    public static void AddDatabaseInitializeService(this IServiceCollection services)
    {
        services.Remove<IDatabaseInitializeService>();
        services.AddScoped<IDatabaseInitializeService, DatabaseEnsureCreateService>();
    }

    // TODO : (dh) AddDbContext를 사용하면 이전 DB Context를 계속 사용하는 버그가 있다. 리포트 필요
    public static void ReplaceTestDbContext<T>(
        this IServiceCollection services, ITestOutputHelper testOutputHelper) where T : DbContext
    {
        services.Remove<T>();

        var connectionString = GetConnectionString(typeof(T).Name);

        services.AddScoped<T>(_ =>
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<T>();
            var optionsBuilder = dbContextOptionsBuilder.UseSqlServer(connectionString)
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString);

            return (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options)!;
        });
    }

    public static void ReplaceTestDbContextWithCrudDatabaseLogging<T>(
        this IServiceCollection services, ITestOutputHelper testOutputHelper) where T : DbContext
    {
        services.Remove<T>();

        var connectionString = GetConnectionString(typeof(T).Name);

        services.AddScoped<T>(_ =>
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<T>();
            var optionsBuilder = dbContextOptionsBuilder.UseSqlServer(connectionString)
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .LogTo(
                    (message) =>
                    {
                        var disables = new List<string> { "DATABASE", "TABLE", "CREATE" };
                        var isContainDisables = disables.Select(message.Contains).ToArray();
                        var isContainDisable = isContainDisables.Any(result => result);
                        
                        var enables = new List<string> { "SELECT", "INSERT", "UPDATE", "DELETE" };
                        var isContainEnables = enables.Select(message.Contains).ToArray();
                        var isContainEnable = isContainEnables.Any(result => result);
                        
                        if (isContainDisable == false && isContainEnable)
                        {
                            testOutputHelper.WriteLine(message);
                        }
                    },
                    new[] { DbLoggerCategory.Database.Command.Name },
                    LogLevel.Information);

            return (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options)!;
        });
    }

#pragma warning disable CS0693
    private static void Remove<T>(this IServiceCollection services)
#pragma warning restore CS0693
    {
        var originSchoolContextDescriptor = services.SingleOrDefault(
            service => service.ServiceType == typeof(T));

        services.Remove(originSchoolContextDescriptor!);
    }

    private static string GetConnectionString(string databaseName)
    {
        var guid = Guid.NewGuid().ToString();
        return
            $"User ID=sa;Password=123aaa!@#;Initial Catalog={databaseName}_{guid};Data Source=localhost;Encrypt=false";
    }

    public static void AddTestConsoleLogger(this ILoggingBuilder builder,
        ITestOutputHelper testOutputHelper)
    {
        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, TestLoggerProvider>(_ =>
                new TestLoggerProvider(testOutputHelper)));
    }
}