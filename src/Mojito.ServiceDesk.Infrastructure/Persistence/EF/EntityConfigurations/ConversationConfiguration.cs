using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojito.ServiceDesk.Core.Entities.Ticketing;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.EntityConfigurations
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> entity)
        {
            entity.ToTable("Conversations", schema: "ticketing");
            entity.HasKey(k => k.Id);
            entity.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
