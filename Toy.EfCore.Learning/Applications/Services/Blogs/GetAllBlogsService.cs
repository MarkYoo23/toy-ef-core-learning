using Toy.EfCore.Learning.Domains.Models.Blogs;
using Toy.EfCore.Learning.Domains.Repositories;

namespace Toy.EfCore.Learning.Applications.Services.Blogs;

public class GetAllBlogsService
{
    private readonly IBlogRepository _blogRepository;

    public GetAllBlogsService(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<IEnumerable<BlogEntity>> GetAllAsync() => await _blogRepository.GetAllAsNoTrackingAsync();
}