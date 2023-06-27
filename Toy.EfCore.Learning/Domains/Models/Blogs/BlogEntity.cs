namespace Toy.EfCore.Learning.Domains.Models.Blogs;

public class BlogEntity
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}