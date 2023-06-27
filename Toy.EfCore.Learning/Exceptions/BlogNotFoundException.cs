
namespace Toy.EfCore.Learning.Exceptions;

public class BlogNotFoundException : Exception, IBadRequestException
{
    public BlogNotFoundException(long id) : base($"Blog (id:{id}) not found ")
    {
    }
}