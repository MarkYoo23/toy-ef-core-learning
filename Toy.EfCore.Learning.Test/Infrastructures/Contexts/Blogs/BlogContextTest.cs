using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Toy.EfCore.Learning.Domains.Models.Blogs;
using Toy.EfCore.Learning.Infrastructures.Contexts;
using Toy.EfCore.Learning.Test.Commons;
using Toy.EfCore.Learning.Test.Commons.Services.Blogs;
using Toy.EfCore.Learning.Test.Commons.TestWorks;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable MethodHasAsyncOverload

namespace Toy.EfCore.Learning.Test.Infrastructures.Contexts.Blogs;

public class BlogContextTest : SeparateApplicationTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BlogContextTest(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Add_ClassRoomWithClassRoomSchedule_ReturnSuccessCreated()
    {
        using var scope = WebApplicationFactory.Services.CreateScope();
        await using var blogContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

        var isSuccess = true;

        try
        {
            var blog = new BlogEntity()
            {
                Url = "https://myblog.com/blog/1"
            };

            await blogContext.Blogs.AddAsync(blog);
            await blogContext.SaveChangesAsync();

            var communityBlog = new CommunityBlogEntity()
            {
                Url = "https://myblog.com/blog/2",
                CommunityName = "blue"
            };

            await blogContext.CommunityBlogs.AddAsync(communityBlog);
            await blogContext.SaveChangesAsync();

            var developBlog = new DevelopBlogEntity()
            {
                Url = "https://myblog.com/blog/3",
                SourceCode = "var x = 1;\nvar y = 2;\nvar z = x + y;"
            };

            await blogContext.DevelopBlogs.AddAsync(developBlog);
            await blogContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _testOutputHelper.WriteLine(e.ToString());
            isSuccess = false;
        }

        Assert.True(isSuccess);
    }

    [Fact]
    public async Task CheckTime_SelectBlogsWithStandardize_ReturnTimeDebug()
    {
        using var scope = WebApplicationFactory.Services.CreateScope();
        await using var blogContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

        blogContext.Blogs.AddRange(BlogsFactory.Create(100000, "localhost/my_blog"));
        blogContext.SaveChanges();

        var query = blogContext.Blogs
            .OrderByDescending(blog => blog.Id)
            .Select(blog => new { Id = blog.Id, Url = blog.Url.BlogUrlStandardize() });

        _testOutputHelper.WriteLine("Next line query string");
        _testOutputHelper.WriteLine(query.ToQueryString());

        var timer = Stopwatch.StartNew();

        var _ = query.ToList();

        timer.Stop();

        _testOutputHelper.WriteLine("-------------------------------------------------");
        _testOutputHelper.WriteLine($"Progress time : {timer.ElapsedMilliseconds} miles");
    }

    [Fact]
    public async Task CheckTime_WhereBlogsWithBlogUrlStandardizeWithContainDotnet_ReturnException()
    {
        using var scope = WebApplicationFactory.Services.CreateScope();
        await using var blogContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

        blogContext.Blogs.AddRange(BlogsFactory.Create(100000, "localhost/my_blog"));
        blogContext.SaveChanges();

        var timer = Stopwatch.StartNew();

        Assert.Throws<InvalidOperationException>(() =>
        {
            var _ = blogContext.Blogs
                .Where(blog => blog.Url.BlogUrlStandardize().Contains("dotnet"))
                .ToList();
        });

        timer.Stop();

        _testOutputHelper.WriteLine("-------------------------------------------------");
        _testOutputHelper.WriteLine($"Progress time : {timer.ElapsedMilliseconds} miles");
    }


    [Fact]
    public async Task CheckTime_SelectAllBlogsWithClientBlogUrlStandardize_ReturnTimeDebug()
    {
        using var scope = WebApplicationFactory.Services.CreateScope();
        await using var blogContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

        blogContext.Blogs.AddRange(BlogsFactory.Create(100000, "localhost/my_blog"));
        blogContext.SaveChanges();

        var timer = Stopwatch.StartNew();

        var _ = blogContext.Blogs
            .AsEnumerable()
            .OrderByDescending(blog => blog.Id)
            .Select(blog => new { Id = blog.Id, Url = blog.Url.BlogUrlStandardize() })
            .ToList();

        timer.Stop();

        _testOutputHelper.WriteLine("-------------------------------------------------");
        _testOutputHelper.WriteLine($"Progress time : {timer.ElapsedMilliseconds} miles");
    }
}