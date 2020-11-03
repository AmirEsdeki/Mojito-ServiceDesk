using Mojito.ServiceDesk.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users", schema: "identity");

            entity.HasKey(k => k.Id);

            entity.HasQueryFilter(p => !p.IsDeleted);

        }
    }
}

