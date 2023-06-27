using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Toy.EfCore.Learning.Domains.Models.Blogs;
using Toy.EfCore.Learning.Infrastructures.Contexts;
using Toy.EfCore.Learning.Test.Commons;
using Toy.EfCore.Learning.Test.Commons.TestWorks;
using Xunit;
using Xunit.Abstractions;

namespace Toy.EfCore.Learning.Test.Infrastructures.Contexts.Blogs;

public class PostTagTest : SeparateApplicationTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public PostTagTest(ITestOutputHelper testOutputHelper)
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
            var tag = new TagEntity()
            {
                Content = "My Life",
            };

            var post = new PostEntity()
            {
                Title = "My Post",
                Content = "This is content",
            };

            tag.Posts = new List<PostEntity>() { post };
            post.Tags = new List<TagEntity>() { tag };

            await blogContext.Tags.AddAsync(tag);
            await blogContext.Posts.AddAsync(post);

            await blogContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _testOutputHelper.WriteLine(e.ToString());
            isSuccess = false;
        }

        Assert.True(isSuccess);
    }
}