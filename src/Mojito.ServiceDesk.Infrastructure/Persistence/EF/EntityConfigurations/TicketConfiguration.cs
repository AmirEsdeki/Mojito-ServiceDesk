using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojito.ServiceDesk.Core.Entities.Ticketing;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.EntityConfigurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> entity)
        {
            entity.ToTable("Tickets", schema: "ticketing");

            entity.Ignore(i => i.HasNextStep);

            entity.HasKey(k => k.Id);

            entity.Property(b => b.Title).HasMaxLength(255);

            entity.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}

