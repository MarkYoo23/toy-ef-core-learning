namespace Toy.EfCore.Learning.Exceptions;

public class DatabaseSaveFailException<T> : Exception, IInternalServerException
{
    public DatabaseSaveFailException(string message = "") : base($"Database save fail at {typeof(T).Name}, {message}")
    {
    }
}