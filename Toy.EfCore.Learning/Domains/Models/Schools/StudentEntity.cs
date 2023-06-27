namespace Toy.EfCore.Learning.Domains.Models.Schools;

public class StudentEntity
{
    public long Id { get; set; }
    
    public DateTime EnrollmentDate { get; set; }
    public DateTime Created { get; set; }

    // 데이터베이스 생성 동시성 토큰
    public byte[]? TimeStampVersion { get; set; }
    // 애플리케이션 관리 동시성 토큰

    public string IgnoreProperty { get; set; } = string.Empty;
    
    public long? SchoolId { get; set; }
    public virtual SchoolEntity? School { get; set; } = null;
    public virtual ICollection<StudentCommuteEntity> StudentCommutes { get; set; } = null!;
    
    public virtual StudentNameEntity? StudentName { get; set; } = null!;
}