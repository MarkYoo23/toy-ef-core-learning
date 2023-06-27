using System.Collections.Generic;
using Toy.EfCore.Learning.Domains.Models.Blogs;

namespace Toy.EfCore.Learning.Test.Commons.Services.Blogs;

public static class PostFactory
{
    public static PostEntity Create(string postTitle, string tag1, string tag2)
    {
        var post = new PostEntity()
        {
            Title = postTitle,
            Content = "content...",
            Tags = new List<TagEntity>()
            {
                new()
                {
                    Content = tag1,
                },
                new()
                {
                    Content = tag2,
                }
            }
        };

        return post;
    }

}