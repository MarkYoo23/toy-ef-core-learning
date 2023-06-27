using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toy.EfCore.Learning.Domains.Models.Schools;
using Toy.EfCore.Learning.Infrastructures.Converters;

namespace Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Schools;

public class SchoolActionLogEntityDefinition: IEntityTypeConfiguration<SchoolActionLogEntity>
{
    public void Configure(EntityTypeBuilder<SchoolActionLogEntity> builder)
    {
        builder.ToTable("school_action");
        ConfigureColumns(builder);
    }
    
    private static void ConfigureColumns(EntityTypeBuilder<SchoolActionLogEntity> builder)
    {
        builder.HasKey(col => col.Id);

        builder.Property(col => col.Id)
            .HasColumnName("id");

        builder.Property(col => col.ChangeAction)
            .HasColumnName("change_action")
            .HasConversion<TableChangeActionConverter>();
        
        builder.Property(col => col.SchoolId)
            .HasColumnName("school_id");

        builder.Property(col => col.Created)
            .HasColumnName("created")
            // ReSharper disable once StringLiteralTypo
            .HasDefaultValueSql("getdate()");
    }
}