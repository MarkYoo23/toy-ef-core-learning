using Toy.EfCore.Learning.Domains.Models.Schools;

namespace Toy.EfCore.Learning.Domains.Repositories;

public interface ISchoolRepository : IGenericRepository<SchoolEntity>
{
    Task<IEnumerable<SchoolEntity>> GetSchoolWithStudentsAsync(string schoolName, string studentName);
}