using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojito.ServiceDesk.Core.Entities.Ticketing;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.EntityConfigurations
{
    public class TicketLabelConfiguration : IEntityTypeConfiguration<TicketLabel>
    {
        public void Configure(EntityTypeBuilder<TicketLabel> entity)
        {
            entity.ToTable("TicketLabels", schema: "ticketing");

            entity.HasKey(k => k.Id);

            entity.Property(b => b.Title).HasMaxLength(255);

            entity.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
