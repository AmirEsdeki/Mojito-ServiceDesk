using Mojito.ServiceDesk.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.EntityConfigurations
{
    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> entity)
        {
            entity.ToTable("UserGroup", schema: "identity");

            entity.HasKey(x => new { x.UserId, x.GroupId });
            entity.HasOne(o => o.User)
                .WithMany(m => m.Groups)
                .HasForeignKey(f => f.UserId);
            entity.HasOne(o => o.Group)
                .WithMany(m => m.Users)
                .HasForeignKey(f => f.GroupId);
        }
    }
}
   