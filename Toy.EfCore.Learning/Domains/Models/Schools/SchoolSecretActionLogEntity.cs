namespace Toy.EfCore.Learning.Domains.Models.Schools;

public class SchoolSecretActionLogEntity
{
    public long Id { get; set; }
    public string Message { get; set; } = string.Empty;

    public DateTime Created { get; set; }

    public Guid SchoolGuid { get; set; }
    public virtual SchoolEntity School { get; set; } = null!;
}