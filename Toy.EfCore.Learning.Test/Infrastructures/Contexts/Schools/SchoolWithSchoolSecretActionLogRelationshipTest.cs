using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Toy.EfCore.Learning.Domains.Models.Schools;
using Toy.EfCore.Learning.Infrastructures.Contexts;
using Toy.EfCore.Learning.Test.Commons;
using Toy.EfCore.Learning.Test.Commons.TestWorks;
using Xunit;
using Xunit.Abstractions;

namespace Toy.EfCore.Learning.Test.Infrastructures.Contexts.Schools;

public class SchoolWithSchoolSecretActionLogRelationshipTest: SeparateApplicationTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public SchoolWithSchoolSecretActionLogRelationshipTest(ITestOutputHelper testOutputHelper) 
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

            var actionLog = new SchoolSecretActionLogEntity()
            {
                Message = "This is secret",
                
                School = school
            };

            await schoolContext.SchoolSecretActionLogs.AddAsync(actionLog);
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