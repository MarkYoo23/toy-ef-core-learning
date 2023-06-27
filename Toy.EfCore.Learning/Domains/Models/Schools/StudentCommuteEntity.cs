namespace Toy.EfCore.Learning.Domains.Models.Schools;

public class StudentCommuteEntity
{
    public long Id { get; set; }
    public string? Fingerprint { get; set; }
    public CommuteType CommuteType { get; set; }
    public DateTime Created { get; set; }

    public long StudentId { get; set; }
    public virtual StudentEntity Student { get; set; } = null!;
}