using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toy.EfCore.Learning.Domains.Models.Blogs;

namespace Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Blogs;

public class PostEntityDefinition : IEntityTypeConfiguration<PostEntity>
{
    public void Configure(EntityTypeBuilder<PostEntity> builder)
    {
        builder.ToTable("post");

        ConfigureColumns(builder);
        ConfigureTagJoinRelationship(builder);
    }

    private static void ConfigureColumns(EntityTypeBuilder<PostEntity> builder)
    {
        builder.HasKey(col => col.Id);

        builder.Property(col => col.Id)
            .HasColumnName("id");

        builder.Property(col => col.Title)
            .HasColumnName("title");

        builder.Property(col => col.Content)
            .HasColumnName("content");

        builder.Property(col => col.Created)
            .HasColumnName("created")
            // ReSharper disable once StringLiteralTypo
            .HasDefaultValueSql("getdate()");
    }

    private static void ConfigureTagJoinRelationship(EntityTypeBuilder<PostEntity> builder)
    {
        builder.HasMany(post => post.Tags)
            .WithMany(tag => tag.Posts)
            .UsingEntity<PostTagEntity>(
                tagBuilder => tagBuilder.HasOne(post => post.Tag)
                    .WithMany()
                    .HasForeignKey(post => post.TagId),
                postBuilder => postBuilder.HasOne(tag => tag.Post)
                    .WithMany()
                    .HasForeignKey(tag => tag.PostId),
                mappingBuilder =>
                {
                    mappingBuilder.Property(mapping => mapping.Created)
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    mappingBuilder.HasOne(mapping => mapping.Post)
                        .WithMany(e => e.PostTags)
                        .HasForeignKey(e => e.PostId);
                    
                    mappingBuilder.HasOne(mapping => mapping.Tag)
                        .WithMany(e => e.PostTags)
                        .HasForeignKey(e => e.TagId);
                });
    }
}