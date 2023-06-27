using Toy.EfCore.Learning.Domains.Models.Blogs;

namespace Toy.EfCore.Learning.Domains.Repositories;

public interface IBlogRepository : IGenericRepository<BlogEntity>
{
    Task<IEnumerable<BlogEntity>> FindBlogsBySqlQueryAsync(string url);
    Task<IEnumerable<BlogEntity>> GetBlogsByProcedureQuery(int count);
    Task<IEnumerable<BlogEntity>> FindBlogsByEfFunctionContainAsync(string url);
}