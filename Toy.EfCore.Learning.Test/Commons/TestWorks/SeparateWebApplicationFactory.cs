using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Toy.EfCore.Learning.Test.Commons.TestWorks;

public class SeparateWebApplicationFactory<T>
    : WebApplicationFactory<T> where T : class
{
    private readonly Action<IServiceCollection> _configureServices;
    private readonly Action<ILoggingBuilder> _loggingBuilder;

    public SeparateWebApplicationFactory(
        Action<IServiceCollection> configureServices,
        Action<ILoggingBuilder> loggingBuilder)
    {
        _configureServices = configureServices;
        _loggingBuilder = loggingBuilder;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(_loggingBuilder);
        builder.ConfigureServices(_configureServices);
        builder.UseEnvironment("Development");
    }
}