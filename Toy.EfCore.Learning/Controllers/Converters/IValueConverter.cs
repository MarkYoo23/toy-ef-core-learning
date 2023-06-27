namespace Toy.EfCore.Learning.Controllers.Converters;

// ReSharper disable once TypeParameterCanBeVariant
public interface IValueConverter<TOrigin, out TResult>
{
    TResult Convert(TOrigin value);
}