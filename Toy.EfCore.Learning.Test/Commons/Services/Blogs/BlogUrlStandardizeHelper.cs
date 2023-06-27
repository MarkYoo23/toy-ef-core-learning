namespace Toy.EfCore.Learning.Test.Commons.Services.Blogs;

public static class BlogUrlStandardizeHelper
{
    public static string BlogUrlStandardize(this string url)
    {
        url = url.ToLower();

        if (!url.StartsWith("http://"))
        {
            url = string.Concat("http://", url);
        }

        return url;
    }
}