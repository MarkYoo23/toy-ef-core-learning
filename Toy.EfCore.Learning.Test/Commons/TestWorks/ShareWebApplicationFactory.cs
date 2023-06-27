using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Toy.EfCore.Learning.Test.Commons.TestWorks;

// 예시용으로 계속 남겨둠
// ReSharper disable once ClassNeverInstantiated.Global
public class ShareWebApplicationFactory : WebApplicationFactory<Program> 
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
    }
}