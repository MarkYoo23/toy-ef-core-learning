using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Toy.EfCore.Learning.Domains.Models.Blogs;
using Toy.EfCore.Learning.Domains.Repositories;
using Toy.EfCore.Learning.Test.Commons.Attributes;
using Toy.EfCore.Learning.Test.Commons.Services.Blogs;
using Toy.EfCore.Learning.Test.Commons.TestWorks;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable MethodHasAsyncOverload

namespace Toy.EfCore.Learning.Test.Infrastructures.Repositories.Blogs;

[ExceptionAutoFailed]
public class BlogRepositoryTest : SeparateApplicationTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BlogRepositoryTest(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Get_BlogsWithTracking_ReturnDebugTime()
    {
        using var scope = WebApplicationFactory.Services.CreateScope();
        var blogRepository = scope.ServiceProvider.GetRequiredService<IBlogRepository>();
        
        var timer = Stopwatch.StartNew();
            
        blogRepository.AddRange(BlogsFactory.Create(100000, "localhost/my_blog"));
        blogRepository.UnitOfWork.SaveChanges();
            
        timer.Stop();
        _testOutputHelper.WriteLine($"Progress time : {timer.ElapsedMilliseconds} miles");
            
        var _ = blogRepository.GetAll();
    }
    
    [Fact]
    public void Get_BlogsWithAsNoTracking_ReturnDebugTime()
    {
        using var scope = WebApplicationFactory.Services.CreateScope();
        var blogRepository = scope.ServiceProvider.GetRequiredService<IBlogRepository>();
        
        var timer = Stopwatch.StartNew();
            
        blogRepository.AddRange(BlogsFactory.Create(100000, "localhost/my_blog"));
        blogRepository.UnitOfWork.SaveChanges();
            
        timer.Stop();
        _testOutputHelper.WriteLine($"Progress time : {timer.ElapsedMilliseconds} miles");
            
        var _ = blogRepository.GetAllAsNoTracking();
    }

    [Fact]
    public async Task Get_FindBlogsBySqlQuery_ReturnSingleBlog()
    {
        var url = "/test_url";
        
        {
            using var scope = WebApplicationFactory.Services.CreateScope();
            var blogRepository = scope.ServiceProvider.GetRequiredService<IBlogRepository>();

            var blog = new BlogEntity()
            {
                Url = url
            };

            await blogRepository.AddAsync(blog);
            await blogRepository.UnitOfWork.SaveChangesAsync();
        }

        {
            using var scope = WebApplicationFactory.Services.CreateScope();
            var blogRepository = scope.ServiceProvider.GetRequiredService<IBlogRepository>();

            var blogs = await blogRepository.FindBlogsBySqlQueryAsync(url);
            Assert.Single(blogs);
            Assert.Equal(url, blogs.First().Url);
        }
    }
    
    [Fact]
    public async Task Get_GetBlogsByProcedureQuery_ReturnSingleBlog()
    {
        var url = "/test_url";
        
        {
            using var scope = WebApplicationFactory.Services.CreateScope();
            var blogRepository = scope.ServiceProvider.GetRequiredService<IBlogRepository>();

            var blog = new BlogEntity()
            {
                Url = url
            };

            await blogRepository.AddAsync(blog);
            await blogRepository.UnitOfWork.SaveChangesAsync();
        }

        {
            using var scope = WebApplicationFactory.Services.CreateScope();
            var blogRepository = scope.ServiceProvider.GetRequiredService<IBlogRepository>();

            var blogs = await blogRepository.GetBlogsByProcedureQuery(1);
            Assert.Single(blogs);
            Assert.Equal(url, blogs.First().Url);
        }
    }
    
    [Fact]
    public async Task Get_FindBlogsByEfFunctionContainAsync_ReturnSingleBlog()
    {
        var url = "/test_url";
        
        {
            using var scope = WebApplicationFactory.Services.CreateScope();
            var blogRepository = scope.ServiceProvider.GetRequiredService<IBlogRepository>();

            var blog = new BlogEntity()
            {
                Url = url
            };

            await blogRepository.AddAsync(blog);
            await blogRepository.UnitOfWork.SaveChangesAsync();
        }

        {
            using var scope = WebApplicationFactory.Services.CreateScope();
            var blogRepository = scope.ServiceProvider.GetRequiredService<IBlogRepository>();

            var blogs = await blogRepository.FindBlogsByEfFunctionContainAsync(url);
            Assert.Single(blogs);
            Assert.Equal(url, blogs.First().Url);
        }
    }
}