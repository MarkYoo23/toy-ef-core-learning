using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toy.EfCore.Learning.Domains.Models.Blogs;

namespace Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Blogs;

public class PostTagEntityDefinition: IEntityTypeConfiguration<PostTagEntity>
{
    public void Configure(EntityTypeBuilder<PostTagEntity> builder)
    {
        builder.ToTable("post_tag_mapping");
        ConfigureColumns(builder);
    }
    
    private static void ConfigureColumns(EntityTypeBuilder<PostTagEntity> builder)
    {
        builder.Property(col => col.PostId)
            .HasColumnName("post_id");

        builder.Property(col => col.TagId)
            .HasColumnName("tag_id");

        builder.Property(col => col.Created)
            .HasColumnName("created");
    }
}