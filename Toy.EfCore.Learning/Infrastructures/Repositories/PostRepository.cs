using Toy.EfCore.Learning.Domains.Models.Blogs;
using Toy.EfCore.Learning.Domains.Repositories;
using Toy.EfCore.Learning.Infrastructures.Contexts;

namespace Toy.EfCore.Learning.Infrastructures.Repositories;

public class PostRepository : GenericRepository<PostEntity>, IPostRepository
{
    public PostRepository(BlogContext context) : base(context)
    {
    }
}