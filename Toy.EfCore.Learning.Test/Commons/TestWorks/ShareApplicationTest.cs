using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Toy.EfCore.Learning.Test.Commons.TestWorks;

// reference : https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0
// https://xunit.net/docs/shared-_context
// IClassFixture는 자원을 공유합니다.
public class ShareApplicationTest : IClassFixture<ShareWebApplicationFactory>, IDisposable
{
    protected readonly ShareWebApplicationFactory WebApplicationFactory;
    private readonly ITestOutputHelper _testOutputHelper;
    
    protected readonly HttpClient Client;
    private readonly TaskCollection _afterRunningTaskCollection;

    protected ShareApplicationTest(
        ShareWebApplicationFactory webApplicationFactory,
        ITestOutputHelper testOutputHelper)
    {
        WebApplicationFactory = webApplicationFactory;
        _testOutputHelper = testOutputHelper;
        Client = webApplicationFactory.CreateClient();
        
        _afterRunningTaskCollection = new();
    }

    protected void RegisterAfterRunning(Func<Task> task) => _afterRunningTaskCollection.Register(task);
    protected void ClearAfterRunning() => _afterRunningTaskCollection.Clear();

    public async void Dispose()
    {
        await _afterRunningTaskCollection.ExecuteAllAsync();
        Client.Dispose();
    }

    protected void Log(string message) => _testOutputHelper.WriteLine(message);
}