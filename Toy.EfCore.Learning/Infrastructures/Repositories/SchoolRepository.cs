using Microsoft.EntityFrameworkCore;
using Toy.EfCore.Learning.Domains.Models.Schools;
using Toy.EfCore.Learning.Domains.Repositories;
using Toy.EfCore.Learning.Infrastructures.Contexts;

namespace Toy.EfCore.Learning.Infrastructures.Repositories;

public class SchoolRepository : GenericRepository<SchoolEntity>, ISchoolRepository
{
    private readonly SchoolContext _context;
    private readonly ILogger<SchoolRepository> _logger;

    public SchoolRepository(
        SchoolContext context,
        ILogger<SchoolRepository> logger) : base(context)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<SchoolEntity>> GetSchoolWithStudentsAsync(
        string schoolName,
        string studentName)
    {
        var query = _context.Schools
            .Where(school => school.Name.Equals(schoolName))
            .OrderByDescending(school => school.Name)
            .Include(school => school.SecretActionLogs)
            .Include(school
                => school.Students
                    .Where(student => student.StudentName!.LastName.Equals(studentName))
                    .OrderByDescending(student => student.StudentName))
            .ThenInclude(student => student.StudentName)
            .Include(school => school.Students)
            .ThenInclude(student => student.StudentCommutes)
            .AsSplitQuery();

        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        // SplitQuery 는 첫번째 쿼리만 현재 반환할 수 있습니다. 이렇게 보려면 데이터베이스 로깅을 확인하는게 좋아보입니다.
#pragma warning disable CA2254
        _logger.LogInformation(query.ToQueryString());
#pragma warning restore CA2254
        
        return await query.ToListAsync();
    }
}