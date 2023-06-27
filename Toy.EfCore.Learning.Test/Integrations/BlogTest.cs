using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Toy.EfCore.Learning.Controllers.Dtos.Blogs;
using Toy.EfCore.Learning.Test.Commons;
using Toy.EfCore.Learning.Test.Commons.Extensions;
using Toy.EfCore.Learning.Test.Commons.TestWorks;
using Xunit;
using Xunit.Abstractions;

namespace Toy.EfCore.Learning.Test.Integrations;

public class BlogTest : ShareApplicationTest
{
    public BlogTest(
        ShareWebApplicationFactory webApplicationFactory, ITestOutputHelper testOutputHelper)
        : base(webApplicationFactory, testOutputHelper)
    {
    }

    [Fact]
    public async Task Execute_AllCRUD_ReturnSuccess()
    {
        var createDto = new BlogCreateDto()
        {
            Url = $"localhost:/{Guid.NewGuid()}",
        };

        var (createdStatusCode, createdResult) = await AddBlogAsync(createDto);
        Assert.Equal(HttpStatusCode.Created, createdStatusCode);
        Assert.Equal(createDto.Url, createdResult.Url);

        // ReSharper disable once AsyncVoidLambda
        RegisterAfterRunning(() => DeleteBlogAsync(createdResult.Id));

        var (getAllStatusCode, getAllResult) = await GetAllBlogsAsync();
        Assert.Equal(HttpStatusCode.OK, getAllStatusCode);
        var getResult = getAllResult.Where(blog => blog.Id == createdResult.Id);
        Assert.Single(getResult);

        var updateDto = new BlogUpdateDto()
        {
            Url = $"localhost:/{Guid.NewGuid()}",
        };

        var (updateStatusCode, updateResult) = await UpdateBlogAsync(createdResult.Id, updateDto);
        Assert.Equal(HttpStatusCode.Created, updateStatusCode);
        Assert.Equal(updateDto.Url, updateResult.Url);

        var (getSingleStatusCode, getSingleResult) = await GetBlogAsync(createdResult.Id);
        Assert.Equal(HttpStatusCode.OK, getSingleStatusCode);
        Assert.Equal(updateDto.Url, getSingleResult.Url);

        ClearAfterRunning();

        var (deleteStatusCode, deleteResult) = await DeleteBlogAsync(createdResult.Id);
        Assert.Equal(HttpStatusCode.OK, deleteStatusCode);
        Assert.Equal(updateDto.Url, deleteResult.Url);
    }

    private async Task<(HttpStatusCode, BlogReadDto[])> GetAllBlogsAsync()
    {
        var responseMessage = await Client.GetAsync("/blogs");
        var content = await responseMessage.Content.ReadAsStringAsync();

        LogResponseMessage(responseMessage, content);

        var dtos = content.Deserialize<BlogReadDto[]>();
        return (responseMessage.StatusCode, dtos)!;
    }

    private async Task<(HttpStatusCode, BlogReadDto)> GetBlogAsync(long id)
    {
        var responseMessage = await Client.GetAsync($"/blogs/{id}");
        var content = await responseMessage.Content.ReadAsStringAsync();

        LogResponseMessage(responseMessage, content);

        var dto = content.Deserialize<BlogReadDto>();
        return (responseMessage.StatusCode, dto)!;
    }

    private async Task<(HttpStatusCode, BlogReadDto)> AddBlogAsync(BlogCreateDto requestContent)
    {
        var responseMessage = await Client.PostAsync(
            "/blogs",
            JsonContent.Create(requestContent));
        var content = await responseMessage.Content.ReadAsStringAsync();

        LogResponseMessage(responseMessage, content);

        var responseContent = content.Deserialize<BlogReadDto>();
        return (responseMessage.StatusCode, responseContent)!;
    }

    private async Task<(HttpStatusCode, BlogReadDto)> UpdateBlogAsync(long id, BlogUpdateDto requestContent)
    {
        var responseMessage = await Client.PatchAsync(
            $"/blogs/{id}",
            JsonContent.Create(requestContent));
        var content = await responseMessage.Content.ReadAsStringAsync();

        LogResponseMessage(responseMessage, content);
        
        var responseContent = content.Deserialize<BlogReadDto>();
        return (responseMessage.StatusCode, responseContent)!;
    }

    private async Task<(HttpStatusCode, BlogReadDto)> DeleteBlogAsync(long id)
    {
        var responseMessage = await Client.DeleteAsync($"/blogs/{id}");
        var content = await responseMessage.Content.ReadAsStringAsync();

        LogResponseMessage(responseMessage, content);
        
        var responseContent = content.Deserialize<BlogReadDto>();
        return (responseMessage.StatusCode, responseContent)!;
    }

    private void LogResponseMessage(HttpResponseMessage message, string content) =>
        Log($"Method : {message.RequestMessage!.RequestUri}:{message.RequestMessage!.Method}, Stat : {message.StatusCode}, Data : {content}");
}