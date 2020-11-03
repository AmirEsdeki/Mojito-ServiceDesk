using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojito.ServiceDesk.Core.Entities.Ticketing;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.EntityConfigurations
{
    public class UserIssueUrlConfiguration : IEntityTypeConfiguration<UserIssueUrl>
    {
        public void Configure(EntityTypeBuilder<UserIssueUrl> entity)
        {
            entity.ToTable("UserIssueUrl", schema: "ticketing");


            entity.HasKey(x => new { x.UserId, x.IssueUrlId });
            entity.HasOne(o => o.User)
                .WithMany(m => m.IssueUrls)
                .HasForeignKey(f => f.UserId);
            entity.HasOne(o => o.IssueUrl)
                .WithMany(m => m.Users)
                .HasForeignKey(f => f.IssueUrlId);
        }
    }
}
