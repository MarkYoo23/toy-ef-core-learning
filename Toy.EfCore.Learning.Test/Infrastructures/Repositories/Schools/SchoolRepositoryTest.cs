using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Toy.EfCore.Learning.Domains.Models.Schools;
using Toy.EfCore.Learning.Domains.Repositories;
using Toy.EfCore.Learning.Test.Commons;
using Toy.EfCore.Learning.Test.Commons.Attributes;
using Toy.EfCore.Learning.Test.Commons.Services.Schools;
using Toy.EfCore.Learning.Test.Commons.TestWorks;
using Xunit;
using Xunit.Abstractions;

namespace Toy.EfCore.Learning.Test.Infrastructures.Repositories.Schools;

[ExceptionAutoFailed]
public class SchoolRepositoryTest : SeparateApplicationTest
{
    public SchoolRepositoryTest(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper, true)
    {
    }

    [Fact]
    public async Task Get_SchoolWithStudents_ReturnSchoolWithStudentsEntities()
    {
        using var scope = WebApplicationFactory.Services.CreateScope();
        var schoolRepository = scope.ServiceProvider.GetRequiredService<ISchoolRepository>();

        var schoolName = "seoul middle school";
        var studentName = "blue";
        
        var isSuccess = true;

        SchoolEntity schoolEntity = null!;
        
        await schoolRepository.AddAsync(SchoolWithStudentFactory.Create(schoolName, studentName));
        await schoolRepository.UnitOfWork.SaveChangesAsync();
            
        schoolEntity = (await schoolRepository.GetSchoolWithStudentsAsync(schoolName, studentName)).First();
        
        Assert.Equal(schoolEntity.Name, schoolName);
    }
}