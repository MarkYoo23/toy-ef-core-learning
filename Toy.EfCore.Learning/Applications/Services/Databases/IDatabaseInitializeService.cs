using Microsoft.EntityFrameworkCore;

namespace Toy.EfCore.Learning.Applications.Services.Databases;

public interface IDatabaseInitializeService
{
    Task InitializeAsync(IEnumerable<DbContext> dbContexts);
}