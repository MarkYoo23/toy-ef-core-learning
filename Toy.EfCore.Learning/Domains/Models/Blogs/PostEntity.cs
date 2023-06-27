namespace Toy.EfCore.Learning.Domains.Models.Blogs;

public class PostEntity
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    
    public virtual ICollection<TagEntity> Tags { get; set; } = null!;
    public virtual ICollection<PostTagEntity> PostTags { get; set; } = null!;
}