using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toy.EfCore.Learning.Domains.Models.Blogs;

namespace Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Blogs;

public class TagEntityDefinition: IEntityTypeConfiguration<TagEntity>
{
    public void Configure(EntityTypeBuilder<TagEntity> builder)
    {
        builder.ToTable("tag");

        ConfigureColumns(builder);
    }
    
    private static void ConfigureColumns(EntityTypeBuilder<TagEntity> builder)
    {
        builder.HasKey(col => col.Id);

        builder.Property(col => col.Id)
            .HasColumnName("id");

        builder.Property(col => col.Content)
            .HasColumnName("content");

        builder.Property(col => col.Created)
            .HasColumnName("created")
            // ReSharper disable once StringLiteralTypo
            .HasDefaultValueSql("getdate()");
    }
}