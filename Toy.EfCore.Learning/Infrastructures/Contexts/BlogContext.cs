using Microsoft.EntityFrameworkCore;
using Toy.EfCore.Learning.Domains.Models.Blogs;
using Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Blogs;

namespace Toy.EfCore.Learning.Infrastructures.Contexts;

public class BlogContext : BaseDbContext
{
    public DbSet<PostEntity> Posts { get; set; } = null!;
    public DbSet<TagEntity> Tags { get; set; } = null!;
    public DbSet<PostTagEntity> PostTags { get; set; } = null!;
    
    public DbSet<BlogEntity> Blogs { get; set; } = null!;
    public DbSet<CommunityBlogEntity> CommunityBlogs { get; set; } = null!;
    public DbSet<DevelopBlogEntity> DevelopBlogs { get; set; } = null!;

    public BlogContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PostEntityDefinition());
        modelBuilder.ApplyConfiguration(new TagEntityDefinition());
        modelBuilder.ApplyConfiguration(new PostTagEntityDefinition());
        
        modelBuilder.ApplyConfiguration(new BlogEntityDefinition());
        modelBuilder.ApplyConfiguration(new CommunityBlogEntityDefinition());
        modelBuilder.ApplyConfiguration(new DevelopBlogEntityDefinition());
    }
}