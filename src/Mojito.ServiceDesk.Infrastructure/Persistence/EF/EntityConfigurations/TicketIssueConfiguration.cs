using Mojito.ServiceDesk.Core.Entities.Ticketing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.EntityConfigurations
{
    public class TicketIssueConfiguration : IEntityTypeConfiguration<TicketIssue>
    {
        public void Configure(EntityTypeBuilder<TicketIssue> entity)
        {
            entity.ToTable("TicketIssues", schema: "ticketing");

            entity.HasKey(k => k.Id);

            entity.Property(b => b.Title).HasMaxLength(500);

            entity.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}

