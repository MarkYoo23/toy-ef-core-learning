using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Toy.EfCore.Learning.Domains.Models;

namespace Toy.EfCore.Learning.Infrastructures.Converters;

// ReSharper disable once ClassNeverInstantiated.Global
public class TableChangeActionConverter : ValueConverter<TableChangeAction, string>
{
    public TableChangeActionConverter() 
        : base(
            action => action.ToString(),
            stringValue => (TableChangeAction)Enum.Parse(typeof(TableChangeAction), stringValue))
    {
    }
}