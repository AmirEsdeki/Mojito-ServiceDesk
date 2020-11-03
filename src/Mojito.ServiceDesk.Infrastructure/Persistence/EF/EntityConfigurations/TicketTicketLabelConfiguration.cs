using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojito.ServiceDesk.Core.Entities.Ticketing;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.EntityConfigurations
{
    public class TicketTicketLabelConfiguration : IEntityTypeConfiguration<TicketTicketLabel>
    {
        public void Configure(EntityTypeBuilder<TicketTicketLabel> entity)
        {
            entity.ToTable("TicketTicketLabel", schema: "ticketing");

            entity.HasKey(k => new { k.TicketId, k.TicketLabelId });

            entity.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
