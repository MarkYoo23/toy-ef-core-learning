using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toy.EfCore.Learning.Domains.Models.Schools;

namespace Toy.EfCore.Learning.Infrastructures.SchemaDefinitions.Schools;

public class SchoolEntityIgnoreDefinition: IEntityTypeConfiguration<SchoolEntity>
{
    public void Configure(EntityTypeBuilder<SchoolEntity> builder)
    {
        builder.ToTable("school", t => t.ExcludeFromMigrations());
    }
}