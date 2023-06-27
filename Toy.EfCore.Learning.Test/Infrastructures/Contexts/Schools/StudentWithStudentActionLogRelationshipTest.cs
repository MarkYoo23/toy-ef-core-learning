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

public class StudentWithStudentActionLogRelationshipTest : SeparateApplicationTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public StudentWithStudentActionLogRelationshipTest(ITestOutputHelper testOutputHelper) 
        : base(testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Add_StudentWithStudentActionLog_ReturnSuccessCreated()
    {
        var isSuccess = true;
        
        try
        {
            using var scope = WebApplicationFactory.Services.CreateScope();
            await using var schoolContext = scope.ServiceProvider.GetRequiredService<SchoolContext>();
        
            var student = new StudentEntity()
            {
                EnrollmentDate = new DateTime(2023, 3, 1),
            };

            await schoolContext.Students.AddAsync(student);
            await schoolContext.SaveChangesAsync();

            var actionLog = new StudentActionLogEntity()
            {
                ChangeAction = TableChangeAction.Create,
                Student = student,
            };

            await schoolContext.StudentActionLogs.AddAsync(actionLog);
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