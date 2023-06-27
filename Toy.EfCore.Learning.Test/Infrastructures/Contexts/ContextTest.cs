using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Toy.EfCore.Learning.Infrastructures.Contexts;
using Xunit;
using Xunit.Abstractions;

namespace Toy.EfCore.Learning.Test.Infrastructures.Contexts;

public class ContextTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ContextTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Request_CreateSchoolDatabase_ReturnSuccessCreated()
    {
        var connectionString =
            $"User ID=sa;Password=123aaa!@#;Initial Catalog=UnitTest_School_{Guid.NewGuid().ToString()};Data Source=localhost;Encrypt=false";
        var dbContextOptionsBuilder = new DbContextOptionsBuilder();
        var optionsBuilder = dbContextOptionsBuilder.UseSqlServer(connectionString);
        var context = new SchoolContext(optionsBuilder.Options);

        var isSuccess = await CreateDatabaseAsync(context);
        await RemoveDatabaseAsync(context);

        Assert.True(isSuccess);
    }

    [Fact]
    public async Task Request_CreateBlogDatabase_ReturnSuccessCreated()
    {
        var connectionString =
            $"User ID=sa;Password=123aaa!@#;Initial Catalog=Blog_{Guid.NewGuid().ToString()};Data Source=localhost;Encrypt=false";
        var dbContextOptionsBuilder = new DbContextOptionsBuilder();
        var optionsBuilder = dbContextOptionsBuilder.UseSqlServer(connectionString);
        var context = new BlogContext(optionsBuilder.Options);

        var isSuccess = await CreateDatabaseAsync(context);
        await RemoveDatabaseAsync(context);

        Assert.True(isSuccess);
    }

    private async Task<bool> CreateDatabaseAsync<T>(T dbContext) where T : DbContext
    {
        var isSuccess = true;

        try
        {
            await dbContext.Database.EnsureCreatedAsync();

            var longDebugString = dbContext.Model.ToDebugString(MetadataDebugStringOptions.LongDefault);
            _testOutputHelper.WriteLine(longDebugString);
        }
        catch (Exception e)
        {
            _testOutputHelper.WriteLine(e.ToString());
            isSuccess = false;
        }

        return isSuccess;
    }

    private static async Task RemoveDatabaseAsync<T>(T dbContext) where T : DbContext
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            await dbContext.Database.EnsureDeletedAsync();
        }
    }
}