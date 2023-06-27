using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Toy.EfCore.Learning.Domains.Models.Blogs;
using Toy.EfCore.Learning.Domains.Repositories;
using Toy.EfCore.Learning.Infrastructures.Contexts;

namespace Toy.EfCore.Learning.Infrastructures.Repositories;

public class BlogRepository : GenericRepository<BlogEntity>, IBlogRepository
{
    private readonly BlogContext _blogContext;
    private readonly ILogger<BlogRepository> _logger;

    // ReSharper disable once SuggestBaseTypeForParameterInConstructor
    public BlogRepository(
        BlogContext blogContext,
        ILogger<BlogRepository> logger) : base(blogContext)
    {
        _blogContext = blogContext;
        _logger = logger;
    }

    public async Task<IEnumerable<BlogEntity>> FindBlogsBySqlQueryAsync(string url)
    {
        var urlColumnName = "url";
        var urlColumnValue = new SqlParameter("urlColumnValue", url);

        var blogs = await _blogContext.Blogs
            .FromSqlRaw($"SELECT * FROM blog where {urlColumnName} = @urlColumnValue", urlColumnValue)
            .ToListAsync();

        return blogs;
    }

    public async Task<IEnumerable<BlogEntity>> GetBlogsByProcedureQuery(int count)
    {
        var blogs = await _blogContext.Blogs.FromSqlRaw($"EXECUTE usp_get_blog {count}").ToListAsync();
        return blogs;
    }

    public async Task<IEnumerable<BlogEntity>> FindBlogsByEfFunctionContainAsync(string url)
    {
        var query = _blogContext.Blogs
            .Where(blog => EF.Functions.Collate(blog.Url, "SQL_Latin1_General_CP1_CS_AS") == url);
        
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        _logger.LogInformation(query.ToQueryString());
        
        var blogs = await query.ToListAsync();

        return blogs;
    }
}