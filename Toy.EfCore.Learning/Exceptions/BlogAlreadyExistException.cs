namespace Toy.EfCore.Learning.Exceptions;

public class BlogAlreadyExistException : Exception, IBadRequestException
{
    public BlogAlreadyExistException(string url) : base($"Blog(url:{url}) already exist")
    {
    }
}