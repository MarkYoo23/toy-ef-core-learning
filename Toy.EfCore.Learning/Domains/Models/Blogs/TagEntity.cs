namespace Toy.EfCore.Learning.Domains.Models.Blogs;

public class TagEntity
{
    public long Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    
    public virtual ICollection<PostEntity> Posts { get; set; } = null!;
    public virtual ICollection<PostTagEntity> PostTags { get; set;} = null!;
}