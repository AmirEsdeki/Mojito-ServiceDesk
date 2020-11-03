using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojito.ServiceDesk.Core.Entities.Ticketing;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.EntityConfigurations
{
    public class IssueUrlConfiguration : IEntityTypeConfiguration<IssueUrl>
    {
        public void Configure(EntityTypeBuilder<IssueUrl> entity)
        {
            entity.ToTable("IssueUrls", schema: "ticketing");

            entity.HasKey(k => k.Id);

            entity.Property(b => b.Url).HasMaxLength(800);

            entity.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
