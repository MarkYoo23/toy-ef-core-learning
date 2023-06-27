using Toy.EfCore.Learning.Infrastructures.Commons;

namespace Toy.EfCore.Learning.Domains.Repositories;

public interface IRepository
{
    IUnitOfWork UnitOfWork { get; }
}