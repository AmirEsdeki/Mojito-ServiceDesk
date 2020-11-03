using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojito.ServiceDesk.Core.Entities.Identity;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.EntityConfigurations
{
    public class CustomerOrganizationConfiguration : IEntityTypeConfiguration<CustomerOrganization>
    {
        public void Configure(EntityTypeBuilder<CustomerOrganization> entity)
        {
            entity.ToTable("CustomerOrganizations", schema: "identity");

            entity.HasKey(k => k.Id);

            entity.Property(b => b.Name).HasMaxLength(500);

            entity.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}

