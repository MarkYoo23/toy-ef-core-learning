namespace Toy.EfCore.Learning.Domains.Models.Schools;

public class ClassRoomScheduleEntity
{
    public long Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Created { get; set; }

    public int Grade { get; set; }
    public int Class { get; set; }
    public virtual ClassRoomEntity ClassRoom { get; set; } = null!;
}