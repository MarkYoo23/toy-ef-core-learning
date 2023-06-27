namespace Toy.EfCore.Learning.Domains.Models.Blogs;

// ReSharper disable once ClassNeverInstantiated.Global
public class PostTagEntity
{
    public long PostId { get; set; }
    public long TagId { get; set; }

    public DateTime Created { get; set; }
    
    public virtual PostEntity Post { get; set; } = null!;
    public virtual TagEntity Tag { get; set; } = null!;
}