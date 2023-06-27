using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Toy.EfCore.Learning.Domains.Repositories;
using Toy.EfCore.Learning.Infrastructures.Commons;
using Toy.EfCore.Learning.Infrastructures.Contexts;

namespace Toy.EfCore.Learning.Infrastructures.Repositories;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly BaseDbContext _context;

    public IUnitOfWork UnitOfWork => _context;

    protected GenericRepository(BaseDbContext context)
    {
        this._context = context;
    }

    public EntityEntry<T> Add(T entity)
        => _context.Set<T>().Add(entity);

    public async Task<EntityEntry<T>> AddAsync(T entity)
        => await _context.Set<T>().AddAsync(entity);

    public void AddRange(IEnumerable<T> entities)
        => _context.Set<T>().AddRange(entities);

    public async Task AddRangeAsync(IEnumerable<T> entities)
        => await _context.Set<T>().AddRangeAsync(entities);

    public IEnumerable<T> FindAll(Expression<Func<T, bool>> expression)
        => _context.Set<T>().Where(expression).ToList();

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression)
        => await _context.Set<T>().Where(expression).ToListAsync();

    public IEnumerable<T> FindAsNoTracking(Expression<Func<T, bool>> expression)
        => _context.Set<T>().Where(expression).AsNoTracking().ToList();

    public async Task<IEnumerable<T>> FindAsNoTrackingAsync(Expression<Func<T, bool>> expression)
        => await _context.Set<T>().Where(expression).AsNoTracking().ToListAsync();
    
    public T FindSingle(Expression<Func<T, bool>> expression)
        => _context.Set<T>().Single(expression);
    public async Task<T> FindSingleAsync(Expression<Func<T, bool>> expression)
        => await _context.Set<T>().SingleAsync(expression);


    public IEnumerable<T> GetAll()
        => _context.Set<T>().ToList();

    public async Task<IEnumerable<T>> GetAllAsync()
        => await _context.Set<T>().ToListAsync();

    public IEnumerable<T> GetAllAsNoTracking()
        => _context.Set<T>().AsNoTracking().ToList();

    public async Task<IEnumerable<T>> GetAllAsNoTrackingAsync()
        => await _context.Set<T>().AsNoTracking().ToListAsync();

    public async Task<T?> GetByIdAsync(long id)
        => await _context.Set<T>().FindAsync(id);

    public EntityEntry<T> Update(T entity)
        => _context.Set<T>().Update(entity);

    public void UpdateRange(IEnumerable<T> entities)
        => _context.Set<T>().UpdateRange(entities);

    public EntityEntry<T> Remove(T entity)
        => _context.Set<T>().Remove(entity);

    public void RemoveRange(IEnumerable<T> entities)
        => _context.Set<T>().RemoveRange(entities);
}