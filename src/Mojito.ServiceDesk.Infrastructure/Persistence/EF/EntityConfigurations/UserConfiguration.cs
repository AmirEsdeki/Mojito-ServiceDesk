using Mojito.ServiceDesk.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users", schema: "identity");

            entity.HasKey(k => k.Id);

            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.MobileNumber).IsUnique();
            entity.HasIndex(e => e.UserName).IsUnique();

            entity.Property(b => b.UserName).HasMaxLength(255);
            entity.Property(b => b.FirstName).HasMaxLength(255);
            entity.Property(b => b.LastName).HasMaxLength(255);
            entity.Property(b => b.Email).HasMaxLength(255);
            entity.Property(b => b.Password).HasMaxLength(500);
            entity.Property(b => b.ForgottenPassCode).HasMaxLength(500);
            entity.Property(b => b.MobileNumber).HasMaxLength(14);

            entity.HasOne(o => o.Role)
                .WithMany(m => m.Users)
                .HasForeignKey(f => f.RoleId)
                .IsRequired();

            entity.HasQueryFilter(p => !p.IsDeleted);

        }
    }
}

