using Microsoft.EntityFrameworkCore.ChangeTracking;
using Toy.EfCore.Learning.Applications.Models.Blogs;
using Toy.EfCore.Learning.Domains.Models.Blogs;
using Toy.EfCore.Learning.Domains.Repositories;
using Toy.EfCore.Learning.Exceptions;

namespace Toy.EfCore.Learning.Applications.Services.Blogs;

public class UpdateBlogService
{
    private readonly IBlogRepository _blogRepository;

    public UpdateBlogService(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<BlogEntity> UpdateAsync(BlogUpdateVo vo)
    {
        var duplicateEntities = await _blogRepository.FindAllAsync(blog => blog.Url.Equals(vo.Url));
        if (duplicateEntities.Any())
        {
            throw new BlogAlreadyExistException(vo.Url);
        }
        
        await using var transaction = await _blogRepository.UnitOfWork.BeginTransactionAsync();

        EntityEntry<BlogEntity> blogEntry;

        try
        {
            var cts = new CancellationTokenSource(3000);

            var entity = await _blogRepository.GetByIdAsync(vo.Id);
            if (entity == null)
            {
                throw new BlogNotFoundException(vo.Id);
            }

            entity.Url = vo.Url;
            blogEntry =_blogRepository.Update(entity);

            var appendLines = await _blogRepository.UnitOfWork.SaveChangesAsync(cts.Token);
            if (appendLines != 1)
            {
                throw new DatabaseSaveFailException<UpdateBlogService>($"Blog (id:{vo.Id}) update fail");
            }

            await transaction.CommitAsync(cts.Token);
        }
        catch(Exception e)
        {
            var cts = new CancellationTokenSource(5000);
            await transaction.RollbackAsync(cts.Token);
            throw;
        }

        return blogEntry.Entity;
    }
}