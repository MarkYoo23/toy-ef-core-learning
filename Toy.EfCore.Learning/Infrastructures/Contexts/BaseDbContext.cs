using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Toy.EfCore.Learning.Infrastructures.Commons;

namespace Toy.EfCore.Learning.Infrastructures.Contexts;

public abstract class BaseDbContext : DbContext, IUnitOfWork
{
    protected BaseDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public IDbContextTransaction BeginTransaction() => Database.BeginTransaction();
    public Task<IDbContextTransaction> BeginTransactionAsync() => Database.BeginTransactionAsync();
}