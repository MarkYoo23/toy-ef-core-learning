namespace Toy.EfCore.Learning.Domains.Models.Schools;

public class SchoolEntity
{
    public long Id { get; set; }
    public Guid GloballyUniqueIdentifier { get; set; }
    public string Name { get; set; } = string.Empty;

    public DateTime Created { get; set; }
    
    public virtual ICollection<StudentEntity> Students { get; set; } = null!;
    public virtual ICollection<SchoolSecretActionLogEntity> SecretActionLogs { get; set; } = null!;
}