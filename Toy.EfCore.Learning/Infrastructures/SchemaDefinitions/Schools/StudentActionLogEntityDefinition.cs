using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toy.EfCore.Learning.Domains.Models.Schools;
using Toy.EfCore.Learning.Infrastructures.Converters;

namespace Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Schools;

public class StudentActionLogEntityDefinition : IEntityTypeConfiguration<StudentActionLogEntity>
{
    public void Configure(EntityTypeBuilder<StudentActionLogEntity> builder)
    {
        builder.ToTable("student_action");

        ConfigureColumns(builder);
        ConfigureStudentRelationship(builder);
    }

    private static void ConfigureColumns(EntityTypeBuilder<StudentActionLogEntity> builder)
    {
        builder.HasKey(col => col.Id);

        builder.Property(col => col.Id)
            .HasColumnName("id");

        builder.Property(col => col.ChangeAction)
            .HasColumnName("change_action")
            .HasConversion<TableChangeActionConverter>();
        
        builder.Property(col => col.Created)
            .HasColumnName("created")
            // ReSharper disable once StringLiteralTypo
            .HasDefaultValueSql("getdate()");
    }
    
    private static void ConfigureStudentRelationship(EntityTypeBuilder<StudentActionLogEntity> builder)
    {
        builder.HasOne(e => e.Student)
            .WithMany()
            .HasForeignKey(e => e.StudentId)
            .IsRequired();
    }
}