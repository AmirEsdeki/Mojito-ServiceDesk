using Mojito.ServiceDesk.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.EntityConfigurations
{
    public class ProfileImageConfiguration : IEntityTypeConfiguration<ProfileImage>
    {
        public void Configure(EntityTypeBuilder<ProfileImage> entity)
        {
            entity.ToTable("ProfileImage", schema: "identity");

            entity.HasKey(k => k.Id);

            entity.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
  