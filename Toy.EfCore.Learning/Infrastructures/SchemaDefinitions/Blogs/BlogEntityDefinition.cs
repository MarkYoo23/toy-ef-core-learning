using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toy.EfCore.Learning.Domains.Models.Blogs;

namespace Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Blogs;

public class BlogEntityDefinition: IEntityTypeConfiguration<BlogEntity>
{
    public void Configure(EntityTypeBuilder<BlogEntity> builder)
    { 
        builder.ToTable("blog");
        ConfigureColumns(builder);
        ConfigureDiscriminators(builder);
    }
    
    private static void ConfigureColumns(EntityTypeBuilder<BlogEntity> builder)
    {
        builder.HasKey(col => col.Id);

        builder.Property(col => col.Id)
            .HasColumnName("id");

        builder.HasIndex(col => col.Url)
            .IsUnique(); 
        
        builder.Property(col => col.Url)
            .HasColumnName("url");

        builder.Property(col => col.Created)
            .HasColumnName("created")
            // ReSharper disable once StringLiteralTypo
            .HasDefaultValueSql("getdate()");
    }
    
    private static void ConfigureDiscriminators(EntityTypeBuilder<BlogEntity> builder)
    {
        builder.HasDiscriminator<string>("blog_type")
            .HasValue<BlogEntity>("blog_default")
            .HasValue<CommunityBlogEntity>("blog_community")
            .HasValue<DevelopBlogEntity>("blog_develop");
    }
}