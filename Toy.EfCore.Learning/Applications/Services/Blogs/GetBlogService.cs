using Toy.EfCore.Learning.Domains.Models.Blogs;
using Toy.EfCore.Learning.Domains.Repositories;
using Toy.EfCore.Learning.Exceptions;

namespace Toy.EfCore.Learning.Applications.Services.Blogs;

public class GetBlogService
{
    private readonly IBlogRepository _blogRepository;

    public GetBlogService(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<BlogEntity> GetAsync(long id)
    {
        var blog = await _blogRepository.GetByIdAsync(id);
        if (blog == null)
        {
            throw new BlogNotFoundException(id);
        }

        return blog;
    }
}