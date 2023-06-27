using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toy.EfCore.Learning.Domains.Models.Schools;

namespace Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Schools;

public class StudentCommuteEntityDefinition : IEntityTypeConfiguration<StudentCommuteEntity>
{
    public void Configure(EntityTypeBuilder<StudentCommuteEntity> builder)
    {
        builder.ToTable("student_commute");

        builder.HasKey(col => col.Id)
            .HasName("PK_StudentCommuteId");

        builder.Property(col => col.Id)
            .HasColumnName("id");

        // 기본 값 예제
        builder.Property(col => col.Fingerprint)
            .HasColumnName("fingerprint")
            .HasDefaultValue(null);

        // 변환용 예제
        builder.Property(col => col.CommuteType)
            .HasColumnName("commute_type");

        // SQL 기본값 예제 
        builder.Property(col => col.Created)
            .HasColumnName("created")
            // ReSharper disable once StringLiteralTypo
            .HasDefaultValueSql("getdate()");
    }
}