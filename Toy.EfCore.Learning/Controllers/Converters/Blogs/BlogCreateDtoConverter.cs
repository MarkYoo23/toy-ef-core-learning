using Toy.EfCore.Learning.Controllers.Dtos.Blogs;
using Toy.EfCore.Learning.Domains.Models.Blogs;

namespace Toy.EfCore.Learning.Controllers.Converters.Blogs;

public class BlogCreateDtoConverter : IValueConverter<BlogCreateDto, BlogEntity>
{
    public BlogEntity Convert(BlogCreateDto value)
    {
        return new BlogEntity()
        {
            Url = value.Url,
        };
    }
}