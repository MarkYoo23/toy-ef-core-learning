using Microsoft.AspNetCore.Mvc;
using Toy.EfCore.Learning.Applications.Services.Blogs;
using Toy.EfCore.Learning.Controllers.Converters.Blogs;
using Toy.EfCore.Learning.Controllers.Dtos.Blogs;

namespace Toy.EfCore.Learning.Controllers;

[ApiController]
[Route("blogs")]
public class BlogController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromServices] GetAllBlogsService service,
        [FromServices] BlogReadDtoConverter converter)
    {
        var entities = await service.GetAllAsync();
        var blogDtos = entities.Select(converter.Convert).ToArray();
        return Ok(blogDtos);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetDetail(
        [FromServices] GetBlogService service,
        [FromServices] BlogReadDtoConverter converter,
        [FromRoute] long id)
    {
        var entity = await service.GetAsync(id);
        var blogDto = converter.Convert(entity!);
        return Ok(blogDto);
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        [FromServices] AddBlogService service,
        [FromServices] BlogCreateDtoConverter createDtoConverter,
        [FromServices] BlogReadDtoConverter readDtoConverter,
        [FromBody] BlogCreateDto dto)
    {
        var notSaveEntity = createDtoConverter.Convert(dto);
        var savedEntity = await service.AddAsync(notSaveEntity);
        var readDto = readDtoConverter.Convert(savedEntity);

        return Created($"blogs/{readDto.Id}", readDto);
    }

    [HttpPatch("{id:long}")]
    public async Task<IActionResult> Update(
        [FromServices] UpdateBlogService service,
        [FromServices] BlogUpdateDtoConverter updateDtoConverter,
        [FromServices] BlogReadDtoConverter readDtoConverter,
        [FromRoute] long id,
        [FromBody] BlogUpdateDto updateDto)
    {
        var request = updateDtoConverter.Convert(id, updateDto);
        var entity = await service.UpdateAsync(request);
        var readDto = readDtoConverter.Convert(entity);
        return Created($"blogs/{readDto!.Id}", readDto);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Update(
        [FromServices] DeleteBlogService service,
        [FromServices] BlogReadDtoConverter readDtoConverter,
        [FromRoute] long id)
    {
        var entity = await service.DeleteAsync(id);
        var readDto = readDtoConverter.Convert(entity);
        return Ok(readDto);
    }
}