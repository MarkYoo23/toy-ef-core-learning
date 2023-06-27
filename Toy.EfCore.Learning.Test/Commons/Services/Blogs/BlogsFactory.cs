using System.Collections.Generic;
using Toy.EfCore.Learning.Domains.Models.Blogs;

namespace Toy.EfCore.Learning.Test.Commons.Services.Blogs;

// Inner Factory -> In a separate class
// https://betterprogramming.pub/5-ways-to-implement-factory-design-pattern-in-c-382c0992a3ff
public static class BlogsFactory
{
    public static List<BlogEntity> Create(long count, string baseUrl)
    {
        var blogs = new List<BlogEntity>();
        
        for (int i = 0; i < count; i++)
        {
            blogs.Add(new BlogEntity()
            {
                Url = @$"{baseUrl}/{i}",
            });
        }

        return blogs;
    }
}