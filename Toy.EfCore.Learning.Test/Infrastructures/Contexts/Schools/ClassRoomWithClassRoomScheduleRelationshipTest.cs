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

public class ClassRoomWithClassRoomScheduleRelationshipTest : SeparateApplicationTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ClassRoomWithClassRoomScheduleRelationshipTest(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Add_ClassRoomWithClassRoomSchedule_ReturnSuccessCreated()
    {
        using var scope = WebApplicationFactory.Services.CreateScope();
        await using var schoolContext = scope.ServiceProvider.GetRequiredService<SchoolContext>();

        var isSuccess = true;

        try
        {
            var classRoom = new ClassRoomEntity()
            {
                Grade = 1,
                Class = 1,
            };

            await schoolContext.ClassRooms.AddAsync(classRoom);
            await schoolContext.SaveChangesAsync();

            var classRoomSchedule = new ClassRoomScheduleEntity()
            {
                StartTime = new DateTime(2023, 1, 1),
                EndTime = new DateTime(2023, 2, 12),
                Title = "winter vacation",

                ClassRoom = classRoom,
            };

            await schoolContext.ClassRoomSchedules.AddAsync(classRoomSchedule);
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