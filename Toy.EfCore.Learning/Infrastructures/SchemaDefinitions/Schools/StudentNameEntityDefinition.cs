using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toy.EfCore.Learning.Domains.Models.Schools;

namespace Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Schools;

public class StudentNameEntityDefinition : IEntityTypeConfiguration<StudentNameEntity>
{
    private const string FirstName = "first_name";
    private const string MidName = "mid_name";
    private const string LastName = "last_name";
    private const string FullName = "full_name";
    
    public void Configure(EntityTypeBuilder<StudentNameEntity> builder)
    {
        builder.ToTable("student_name");
        
        ConfigureColumns(builder);
    }
    
    private static void ConfigureColumns(EntityTypeBuilder<StudentNameEntity> builder)
    {
        builder.HasKey(col => col.Id);

        builder.Property(col => col.Id)
            .HasColumnName("id");

        builder.Property(col => col.FirstName)
            .HasColumnName(FirstName)
            .HasMaxLength(20)
            .IsUnicode()
            .HasComment("This is personal name")
            .HasColumnOrder(0);

        builder.Property(col => col.MidName)
            .HasColumnName(MidName)
            .HasMaxLength(20)
            .IsUnicode()
            .HasComment("What is mid name?")
            .HasColumnOrder(1);

        builder.Property(col => col.LastName)
            .HasColumnName(LastName)
            .HasMaxLength(20)
            .IsUnicode()
            .HasComment("This is family name")
            .HasColumnOrder(2);

        builder.Property(col => col.FullName)
            .HasColumnName(FullName)
            .HasMaxLength(60)
            .IsUnicode()
            .HasComment("This is full name")
            .HasComputedColumnSql($"[{FirstName}] + ' ' + [{MidName}] + ' ' + [{LastName}]");

        builder.Property(col => col.FullNameLength)
            .HasColumnName("full_name_length")
            .HasComment("This is full name length")
            .HasComputedColumnSql($"LEN([{FirstName}]) + LEN([{MidName}]) + LEN([{MidName}])", stored: true);

        builder.Property(col => col.StudentId)
            .HasColumnName("student_id");
    }
}