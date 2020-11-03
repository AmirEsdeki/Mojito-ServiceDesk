using Mojito.ServiceDesk.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.EntityConfigurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> entity)
        {
            entity.ToTable("Groups", schema: "identity");

            entity.HasKey(k => k.Id);

            entity.HasIndex(h => h.Name).IsUnique().IsClustered(false);
            entity.Property(b => b.Name).HasMaxLength(255);

            entity.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
