using Microsoft.EntityFrameworkCore;
using Toy.EfCore.Learning.Domains.Models.Schools;
using Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Schools;

namespace Toy.EfCore.Learning.Infrastructures.Contexts;

public class SchoolContext : BaseDbContext
{
    public DbSet<SchoolEntity> Schools { get; set; } = null!;
    public DbSet<SchoolActionLogEntity> SchoolActionLogs { get; set; } = null!;
    public DbSet<SchoolSecretActionLogEntity> SchoolSecretActionLogs { get; set; } = null!;

    public DbSet<ClassRoomEntity> ClassRooms { get; set; } = null!;
    public DbSet<ClassRoomScheduleEntity> ClassRoomSchedules { get; set; } = null!;

    public DbSet<StudentEntity> Students { get; set; } = null!;
    public DbSet<StudentNameEntity> StudentNames { get; set; } = null!;
    public DbSet<StudentActionLogEntity> StudentActionLogs { get; set; } = null!;
    public DbSet<StudentCommuteEntity> StudentCommutes { get; set; } = null!;

    public SchoolContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SchoolEntityDefinition());
        modelBuilder.ApplyConfiguration(new SchoolActionLogEntityDefinition());
        modelBuilder.ApplyConfiguration(new SchoolSecretActionLogEntityDefinition());

        modelBuilder.ApplyConfiguration(new ClassRoomEntityDefinition());
        modelBuilder.ApplyConfiguration(new ClassRoomScheduleEntityDefinition());

        modelBuilder.ApplyConfiguration(new StudentEntityDefinition());
        modelBuilder.ApplyConfiguration(new StudentNameEntityDefinition());
        modelBuilder.ApplyConfiguration(new StudentActionLogEntityDefinition());
        modelBuilder.ApplyConfiguration(new StudentCommuteEntityDefinition());
    }
}