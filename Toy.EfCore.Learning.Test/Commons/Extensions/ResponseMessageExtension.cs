using System;
using Newtonsoft.Json;

namespace Toy.EfCore.Learning.Test.Commons.Extensions;

public static class ResponseMessageExtension
{
    public static T Deserialize<T>(this string content) where T : class
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return null!;
        }

        T? result = null!;

        try
        {
            result = JsonConvert.DeserializeObject<T>(content);
        }
        catch (Exception)
        {
            // ignored
        }
        
        return result!;
    }
}