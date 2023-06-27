using Toy.EfCore.Learning.Applications.Models.Blogs;
using Toy.EfCore.Learning.Controllers.Dtos.Blogs;

namespace Toy.EfCore.Learning.Controllers.Converters.Blogs;

public class BlogUpdateDtoConverter
{
    public BlogUpdateVo Convert(long id, BlogUpdateDto dto)
    {
        return new BlogUpdateVo()
        {
            Id = id,
            Url = dto.Url
        };
    }
}