namespace Toy.EfCore.Learning.Domains.Models.Schools;

public class StudentNameEntity
{
    public long Id { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? MidName { get; set; }
    public string FirstName { get; set; } = string.Empty;

    public string? FullName { get; set; }
    public int FullNameLength { get; set; }

    public long StudentId { get; set; }
    public virtual StudentEntity Student { get; set; } = null!;
}