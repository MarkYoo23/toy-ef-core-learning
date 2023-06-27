using System;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Toy.EfCore.Learning.Test.Commons.Loggers;

public class TestLogger : ILogger
{
    private readonly string _name;
    private readonly ITestOutputHelper _testOutputHelper;

    public TestLogger(
        string name,
        ITestOutputHelper testOutputHelper)
    {
        _name = name;
        _testOutputHelper = testOutputHelper;
    }
    
#pragma warning disable CS8633
#pragma warning disable CS8767
    public IDisposable BeginScope<TState>(TState state) where TState : notnull => default!;
#pragma warning restore CS8767
#pragma warning restore CS8633

    public void Log<TState>(
        LogLevel logLevel, 
        EventId eventId, 
        TState state, 
        Exception? exception, 
        Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }
        
        _testOutputHelper.WriteLine($"[{eventId.Id,2}: {logLevel,-12}]");
        _testOutputHelper.WriteLine($"     {_name} - {formatter(state, exception!)}");
    }
    
    public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;
}