using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toy.EfCore.Learning.Domains.Models.Blogs;

namespace Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Blogs;

public class CommunityBlogEntityDefinition: IEntityTypeConfiguration<CommunityBlogEntity>
{
    public void Configure(EntityTypeBuilder<CommunityBlogEntity> builder)
    {
        ConfigureColumns(builder);
    }

    private static void ConfigureColumns(EntityTypeBuilder<CommunityBlogEntity> builder)
    {
        builder.Property(col => col.CommunityName)
            .HasColumnName("community_name");
    }
}