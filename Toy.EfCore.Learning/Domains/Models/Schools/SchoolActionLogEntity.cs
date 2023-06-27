namespace Toy.EfCore.Learning.Domains.Models.Schools;

public class SchoolActionLogEntity
{
    public long Id { get; set; }
    public TableChangeAction ChangeAction { get; set; }
    public DateTime Created { get; set; }

    public long SchoolId { get; set; }
}