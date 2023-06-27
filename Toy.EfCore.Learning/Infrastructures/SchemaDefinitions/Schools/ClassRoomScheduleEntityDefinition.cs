using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toy.EfCore.Learning.Domains.Models.Schools;

namespace Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Schools;

public class ClassRoomScheduleEntityDefinition : IEntityTypeConfiguration<ClassRoomScheduleEntity>
{
    public void Configure(EntityTypeBuilder<ClassRoomScheduleEntity> builder)
    {
        builder.ToTable("class_room_schedule");

        ConfigureColumns(builder);
    }
    
    private static void ConfigureColumns(EntityTypeBuilder<ClassRoomScheduleEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(col => col.StartTime)
            .HasColumnName("start_time");

        builder.Property(col => col.EndTime)
            .HasColumnName("end_time");

        builder.Property(col => col.Title)
            .HasColumnName("title");
        
        builder.Property(col => col.Created)
            .HasColumnName("created")
            // ReSharper disable once StringLiteralTypo
            .HasDefaultValueSql("getdate()");
    }
}