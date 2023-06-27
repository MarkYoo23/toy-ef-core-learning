namespace Toy.EfCore.Learning.Controllers.Dtos.Blogs;

public class BlogReadDto
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}