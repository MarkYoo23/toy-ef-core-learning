using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Toy.EfCore.Learning.Test.Commons.Loggers;

public class TestLoggerProvider : ILoggerProvider
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    public TestLoggerProvider(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    public void Dispose()
    {
    }

    public ILogger CreateLogger(string categoryName) => new TestLogger(categoryName, _testOutputHelper);
}
