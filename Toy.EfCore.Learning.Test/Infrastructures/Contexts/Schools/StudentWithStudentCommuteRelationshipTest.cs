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

public class StudentWithStudentCommuteRelationshipTest : SeparateApplicationTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public StudentWithStudentCommuteRelationshipTest(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Add_GoToWorkCommute_ReturnSuccessCreated()
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

            var goToWorkCommute = new StudentCommuteEntity()

            {
                CommuteType = CommuteType.GoToWork,
                Fingerprint = "fingerprint",

                Student = student,
            };

            await schoolContext.StudentCommutes.AddAsync(goToWorkCommute);
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