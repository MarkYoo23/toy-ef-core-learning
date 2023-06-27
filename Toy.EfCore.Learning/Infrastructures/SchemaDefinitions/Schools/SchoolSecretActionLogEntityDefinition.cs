using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toy.EfCore.Learning.Domains.Models.Schools;

namespace Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Schools;

public class SchoolSecretActionLogEntityDefinition : IEntityTypeConfiguration<SchoolSecretActionLogEntity>
{
    public void Configure(EntityTypeBuilder<SchoolSecretActionLogEntity> builder)
    {
        builder.ToTable("school_secret_action");
        ConfigureColumns(builder);
    }
    
    private static void ConfigureColumns(EntityTypeBuilder<SchoolSecretActionLogEntity> builder)
    {
        builder.HasKey(col => col.Id);

        builder.Property(col => col.Id)
            .HasColumnName("id");
        
        builder.Property(col => col.Message)
            .HasColumnName("message");
        
        builder.Property(col => col.Created)
            .HasColumnName("created")
            // ReSharper disable once StringLiteralTypo
            .HasDefaultValueSql("getdate()");
    }
}