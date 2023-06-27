using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Toy.EfCore.Learning.Domains.Models.Schools;

namespace Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Schools;

public class SchoolEntityDefinition : IEntityTypeConfiguration<SchoolEntity>
{
    public void Configure(EntityTypeBuilder<SchoolEntity> builder)
    {
        builder.ToTable("school");

        ConfigureColumns(builder);
        ConfigureStudentRelationship(builder);
        ConfigureSchoolActionLogRelationship(builder);
        ConfigureSchoolSecretActionLogRelationship(builder);
    }

    private static void ConfigureColumns(EntityTypeBuilder<SchoolEntity> builder)
    {
        builder.HasKey(col => col.Id);

        builder.HasIndex(col => col.GloballyUniqueIdentifier)
            .IsUnique();

        builder.Property(col => col.Id)
            .HasColumnName("id");

        builder.Property(col => col.GloballyUniqueIdentifier)
            .HasColumnName("globally_unique_identifier")
            .HasValueGenerator(typeof(SequentialGuidValueGenerator));

        builder.Property(col => col.Name)
            .HasColumnName("name");

        builder.Property(col => col.Created)
            .HasColumnName("created")
            // ReSharper disable once StringLiteralTypo
            .HasDefaultValueSql("getdate()");
    }

    private static void ConfigureStudentRelationship(EntityTypeBuilder<SchoolEntity> builder)
    {
        builder.HasMany(e => e.Students)
            .WithOne(e => e.School)
            .HasForeignKey(e => e.SchoolId)
            .IsRequired(false);
    }

    private static void ConfigureSchoolActionLogRelationship(EntityTypeBuilder<SchoolEntity> builder)
    {
        builder.HasMany<SchoolActionLogEntity>()
            .WithOne()
            .HasForeignKey(e => e.SchoolId)
            // 아래와 같이 키 명을 지정 할 수 있다.
            // .HasConstraintName("FK...")
            .IsRequired();
    }

    private static void ConfigureSchoolSecretActionLogRelationship(EntityTypeBuilder<SchoolEntity> builder)
    {
        builder.HasMany(e => e.SecretActionLogs)
            .WithOne(e => e.School)
            .HasPrincipalKey(e => e.GloballyUniqueIdentifier)
            .HasForeignKey(e => e.SchoolGuid)
            .IsRequired();
    }
}