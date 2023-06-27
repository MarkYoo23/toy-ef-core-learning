using System.Collections.Generic;
using Toy.EfCore.Learning.Domains.Models.Schools;

namespace Toy.EfCore.Learning.Test.Commons.Services.Schools;

public static class SchoolWithStudentFactory
{
    public static SchoolEntity Create(string schoolName, string studentName)
    {
        var school = new SchoolEntity()
        {
            Name = schoolName,
            Students = new List<StudentEntity>()
            {
                new()
                {
                    StudentName = new StudentNameEntity()
                    {
                        FirstName = "f",
                        MidName = "m",
                        LastName = studentName,
                    },
                    StudentCommutes = new List<StudentCommuteEntity>()
                    {
                        new()
                        {
                            CommuteType = CommuteType.GoToWork,
                            Fingerprint = "finger~"
                        },
                        new()
                        {
                            CommuteType = CommuteType.GoToHome,
                            Fingerprint = "finger~"
                        },
                    }
                }
            },
            SecretActionLogs = new List<SchoolSecretActionLogEntity>()
            {
                new()
                {
                    Message = "action_log_1"
                },
                new()
                {
                    Message = "action_log_2"
                }
            }
        };

        return school;
    }
}