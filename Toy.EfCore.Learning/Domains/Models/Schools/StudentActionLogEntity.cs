// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Toy.EfCore.Learning.Domains.Models.Schools;

// Updated 로그를 직접적으로 남길 수 없으므로 우회 방법
public class StudentActionLogEntity
{
    public long Id { get; set; }
    public TableChangeAction ChangeAction { get; set; }
    public DateTime Created { get; set; }

    public long StudentId { get; set; }
    public virtual StudentEntity Student { get; set; } = null!;
}