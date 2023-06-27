using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Toy.EfCore.Learning.Domains.Repositories;
using Toy.EfCore.Learning.Test.Commons;
using Toy.EfCore.Learning.Test.Commons.Services.Blogs;
using Toy.EfCore.Learning.Test.Commons.TestWorks;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable MethodHasAsyncOverload

namespace Toy.EfCore.Learning.Test.Infrastructures.Repositories.Blogs;

public class PostRepositoryTest : SeparateApplicationTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public PostRepositoryTest(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Get_PostWithLazyLoading_ReturnLazyLoading()
    {
        var isSuccess = true;

        var postTitle = "postTitle";
        var tag1 = "tag1";
        var tag2 = "tag2";

        try
        {
            using var scope = WebApplicationFactory.Services.CreateScope();
            var postRepository = scope.ServiceProvider.GetRequiredService<IPostRepository>();
            
            await postRepository.AddAsync(PostFactory.Create(postTitle, tag1, tag2));
            await postRepository.UnitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _testOutputHelper.WriteLine(e.ToString());
            isSuccess = false;
        }
        
        Assert.True(isSuccess);

        try
        {
            using var scope = WebApplicationFactory.Services.CreateScope();
            var postRepository = scope.ServiceProvider.GetRequiredService<IPostRepository>();
            
            var post = await postRepository.FindSingleAsync(post => post.Title.Equals(postTitle));
            _testOutputHelper.WriteLine($"Get posts done");
            
            foreach (var tag in post.Tags)
            {
                _testOutputHelper.WriteLine($"tag : {tag.Content}");
            }
        }
        catch (Exception e)
        {
            _testOutputHelper.WriteLine(e.ToString());
            isSuccess = false;
        }
        
        Assert.True(isSuccess);
    }
}