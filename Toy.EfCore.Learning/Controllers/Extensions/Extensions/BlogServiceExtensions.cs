using Toy.EfCore.Learning.Applications.Services.Blogs;
using Toy.EfCore.Learning.Controllers.Converters.Blogs;

namespace Toy.EfCore.Learning.Controllers.Extensions.Extensions;

public static class BlogServiceExtensions
{
    public static void AddBlogServices(this IServiceCollection services)
    {
        services.AddScoped<GetAllBlogsService>();
        services.AddScoped<GetBlogService>();
        services.AddScoped<AddBlogService>();
        services.AddScoped<UpdateBlogService>();
        services.AddScoped<DeleteBlogService>();

        services.AddScoped<BlogCreateDtoConverter>();
        services.AddScoped<BlogReadDtoConverter>();
        services.AddScoped<BlogUpdateDtoConverter>();
    }
}