using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toy.EfCore.Learning.Domains.Models.Schools;

namespace Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Schools;

public class ClassRoomEntityDefinition : IEntityTypeConfiguration<ClassRoomEntity>
{
    public void Configure(EntityTypeBuilder<ClassRoomEntity> builder)
    {
        builder.ToTable("class_room");
        
        ConfigureColumns(builder);
        ConfigureClassRoomScheduleRelationship(builder);
    }

    private static void ConfigureColumns(EntityTypeBuilder<ClassRoomEntity> builder)
    {
        builder.HasKey(e => new { e.Grade, e.Class });

        builder.Property(col => col.Grade)
            .HasColumnName("grade");

        builder.Property(col => col.Class)
            .HasColumnName("class");

        builder.Property(col => col.Created)
            .HasColumnName("created")
            // ReSharper disable once StringLiteralTypo
            .HasDefaultValueSql("getdate()");
    }

    private static void ConfigureClassRoomScheduleRelationship(
        EntityTypeBuilder<ClassRoomEntity> builder)
    {
        builder.HasMany(e => e.ClassRoomSchedules)
            .WithOne(e => e.ClassRoom)
            .HasForeignKey(e => new { e.Grade, e.Class })
            .IsRequired();
    }
}