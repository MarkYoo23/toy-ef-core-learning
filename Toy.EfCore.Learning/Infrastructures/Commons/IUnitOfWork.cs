using Microsoft.EntityFrameworkCore.Storage;

namespace Toy.EfCore.Learning.Infrastructures.Commons;

// 레퍼런스 문서 : https://learn.microsoft.com/ko-kr/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
// 레퍼런스 프로젝트 : https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/Services/Ordering/Ordering.Domain/SeedWork/IUnitOfWork.cs
public interface IUnitOfWork : IDisposable
{
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    IDbContextTransaction BeginTransaction();
    Task<IDbContextTransaction> BeginTransactionAsync();
}