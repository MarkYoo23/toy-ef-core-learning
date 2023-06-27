using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Toy.EfCore.Learning.Domains.Models;
using Toy.EfCore.Learning.Domains.Models.Schools;
using Toy.EfCore.Learning.Infrastructures.Contexts;
using Toy.EfCore.Learning.Test.Commons;
using Toy.EfCore.Learning.Test.Commons.TestWorks;
using Xunit;
using Xunit.Abstractions;

namespace Toy.EfCore.Learning.Test.Infrastructures.Contexts.Schools;

public class SchoolWithSchoolActionLogRelationshipTest : SeparateApplicationTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public SchoolWithSchoolActionLogRelationshipTest(ITestOutputHelper testOutputHelper) 
        : base(testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Add_SchoolWithSchoolActionLog_ReturnSuccessCreated()
    {
        using var scope = WebApplicationFactory.Services.CreateScope();
        await using var schoolContext = scope.ServiceProvider.GetRequiredService<SchoolContext>();

        var isSuccess = true;
        
        try
        {
            var school = new SchoolEntity()
            {
                Name = "New Gettysburg",
            };

            await schoolContext.Schools.AddAsync(school);
            await schoolContext.SaveChangesAsync();

            var actionLog = new SchoolActionLogEntity()
            {
                ChangeAction = TableChangeAction.Create,
                SchoolId = school.Id,
            };

            await schoolContext.SchoolActionLogs.AddAsync(actionLog);
            await schoolContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _testOutputHelper.WriteLine(e.ToString());
            isSuccess = false;
        }
        
        Assert.True(isSuccess);
    }
}