using System;
using Xunit;
using Toy.EfCore.Learning.Test.Commons.TestWorks;
using Xunit.Abstractions;


namespace Toy.EfCore.Learning.Test;

public class EnvironmentTest : SeparateApplicationTest
{
    public EnvironmentTest(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
    }

    [Fact]
    public void Get_AspnetcoreEnvironmentValue_ReturnNull()
    {
        var aspnetcoreEnvironmentValue = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        Assert.Null(aspnetcoreEnvironmentValue);
    }
}