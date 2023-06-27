using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toy.EfCore.Learning.Domains.Models.Blogs;

namespace Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Blogs;

public class DevelopBlogEntityDefinition: IEntityTypeConfiguration<DevelopBlogEntity>
{
    public void Configure(EntityTypeBuilder<DevelopBlogEntity> builder)
    {
        ConfigureColumns(builder);
        
    }

    private static void ConfigureColumns(EntityTypeBuilder<DevelopBlogEntity> builder)
    {
        builder.Property(col => col.SourceCode)
            .HasColumnName("source_code");
    }
}