using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mojito.ServiceDesk.Core.Entities.Proprietary;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Products", schema: "proprietary");

            entity.HasKey(k => k.Id);

            entity.HasIndex(h => h.Name).IsUnique().IsClustered(false);
            entity.Property(b => b.Name).HasMaxLength(255);

            entity.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
