using Microsoft.EntityFrameworkCore.ChangeTracking;
using Toy.EfCore.Learning.Domains.Models.Blogs;
using Toy.EfCore.Learning.Domains.Repositories;
using Toy.EfCore.Learning.Exceptions;

namespace Toy.EfCore.Learning.Applications.Services.Blogs;

public class DeleteBlogService
{
    private readonly IBlogRepository _blogRepository;

    public DeleteBlogService(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<BlogEntity> DeleteAsync(long id)
    {
        var entity = await _blogRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new BlogNotFoundException(id);
        }

        await using var transaction = await _blogRepository.UnitOfWork.BeginTransactionAsync();

        EntityEntry<BlogEntity> blogEntry = null!;

        try
        {
            var cts = new CancellationTokenSource(3000);

            blogEntry = _blogRepository.Remove(entity);
            var appendRows = await _blogRepository.UnitOfWork.SaveChangesAsync(cts.Token);
            if (appendRows != 1)
            {
                throw new DatabaseSaveFailException<DeleteBlogService>($"Blog (id:{id}) delete fail");
            }
            
            await transaction.CommitAsync(cts.Token);
        }
        catch (Exception e)
        {            
            var cts = new CancellationTokenSource(5000);
            await transaction.RollbackAsync(cts.Token);
            throw;
        }

        return blogEntry.Entity;
    }
}