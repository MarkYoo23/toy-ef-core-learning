using Toy.EfCore.Learning.Controllers.Dtos.Blogs;
using Toy.EfCore.Learning.Domains.Models.Blogs;

namespace Toy.EfCore.Learning.Controllers.Converters.Blogs;

public class BlogReadDtoConverter : IValueConverter<BlogEntity, BlogReadDto>
{
    public BlogReadDto Convert(BlogEntity value)
    {
        return new BlogReadDto()
        {
            Id = value.Id,
            Url = value.Url,
            Created = value.Created,
        };
    }
}