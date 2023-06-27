using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toy.EfCore.Learning.Domains.Models.Schools;

// ReSharper disable StringLiteralTypo

namespace Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Schools;

public class StudentEntityDefinition : IEntityTypeConfiguration<StudentEntity>
{
    private const string StudentTableName = "student";

    public void Configure(EntityTypeBuilder<StudentEntity> builder)
    {
        builder.ToTable(StudentTableName);
        ConfigureColumns(builder);
        ConfigureStudentCommuteRelationship(builder);
        ConfigureStudentNameRelationship(builder);
    }

    private static void ConfigureColumns(EntityTypeBuilder<StudentEntity> builder)
    {
        builder.HasKey(col => col.Id)
            .HasName("PK_StudentId");

        builder.Property(col => col.Id)
            .HasColumnName("id");

        builder.Property(col => col.EnrollmentDate)
            .HasColumnName("enrollment_date");

        builder.Property(col => col.Created)
            .HasColumnName("created")
            .HasDefaultValueSql("getdate()");

        builder.Property(col => col.TimeStampVersion)
            .HasColumnName("timestamp_version")
            .IsRowVersion();

        builder.Ignore(col => col.IgnoreProperty);

        builder.Property(col => col.SchoolId)
            .HasColumnName("school_id");
    }

    private static void ConfigureStudentCommuteRelationship(EntityTypeBuilder<StudentEntity> builder)
    {
        builder.HasMany<StudentCommuteEntity>(e => e.StudentCommutes)
            .WithOne(e => e.Student)
            .HasForeignKey(e => e.StudentId)
            .IsRequired();
    }

    private static void ConfigureStudentNameRelationship(EntityTypeBuilder<StudentEntity> builder)
    {
        builder.HasOne(e => e.StudentName)
            .WithOne(e => e.Student)
            .HasForeignKey<StudentNameEntity>(e => e.StudentId)
            .IsRequired(false);
    }
}