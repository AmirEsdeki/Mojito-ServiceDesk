using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojito.ServiceDesk.Core.Entities.Ticketing;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.EntityConfigurations
{
    public class TicketAttachmentConfiguration : IEntityTypeConfiguration<TicketAttachment>
    {
        public void Configure(EntityTypeBuilder<TicketAttachment> entity)
        {
            entity.ToTable("TicketAttachments", schema: "ticketing");

            entity.HasKey(k => k.Id);

            entity.Property(b => b.Location).HasMaxLength(1500);

            entity.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
