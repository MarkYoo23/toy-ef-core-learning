using Microsoft.EntityFrameworkCore.ChangeTracking;
using Toy.EfCore.Learning.Domains.Models.Blogs;
using Toy.EfCore.Learning.Domains.Repositories;
using Toy.EfCore.Learning.Exceptions;
// ReSharper disable MethodHasAsyncOverloadWithCancellation

// ReSharper disable MethodHasAsyncOverload

namespace Toy.EfCore.Learning.Applications.Services.Blogs;

public class AddBlogService
{
    private readonly IBlogRepository _blogRepository;

    public AddBlogService(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<BlogEntity> AddAsync(BlogEntity entity)
    {
        var duplicateEntities = await _blogRepository.FindAllAsync(
            blog => blog.Url.Equals(entity.Url));
        if (duplicateEntities.Any())
        {
            throw new BlogAlreadyExistException(entity.Url);
        }

        EntityEntry<BlogEntity> entry = null!;
        
        await using var transaction = _blogRepository.UnitOfWork.BeginTransaction();

        try
        {        
            var cts = new CancellationTokenSource(3000);

            entry = await _blogRepository.AddAsync(entity);

            var appendLines = await _blogRepository.UnitOfWork.SaveChangesAsync(cts.Token);
            if (appendLines != 1)
            {
                throw new DatabaseSaveFailException<AddBlogService>("Blog save fail");
            }

            transaction.Commit();
        }
        catch
        {
            var cts = new CancellationTokenSource(5000);
            transaction.Rollback();
            throw;
        }

        return entry.Entity;
    }
}