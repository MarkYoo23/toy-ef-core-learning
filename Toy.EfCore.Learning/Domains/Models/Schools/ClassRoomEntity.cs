namespace Toy.EfCore.Learning.Domains.Models.Schools;

public class ClassRoomEntity
{
    public int Grade { get; set; }
    public int Class { get; set; }
    public DateTime Created { get; set; }

    public virtual ICollection<ClassRoomScheduleEntity> ClassRoomSchedules { get; set; } = null!;
}