using Mojito.ServiceDesk.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.EntityConfigurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> entity)
        {
            entity.ToTable("Posts", schema: "identity");

            entity.HasKey(k => k.Id);

            entity.HasIndex(h => h.Title).IsUnique().IsClustered(false);
            entity.Property(b => b.Title).HasMaxLength(255);

            entity.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
