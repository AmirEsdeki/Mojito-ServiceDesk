using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mojito.ServiceDesk.Application.Common.Interfaces.Common;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;
using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using Mojito.ServiceDesk.Core.Entities.Identity;
using Mojito.ServiceDesk.Core.Entities.Proprietary;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Data.EF
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        private readonly IDateTimeService dateTime;
        private readonly IAppUser appUser;

        public ApplicationDBContext(DbContextOptions options, IDateTimeService dateTime, IAppUser appUser) : base(options)
        {
            this.dateTime = dateTime;
            this.appUser = appUser;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Configuration
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
            builder.HasDefaultSchema("identity");
            base.OnModelCreating(builder);
            #endregion
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            Guid? userId = null;
            if (appUser.Id != null)
                userId = Guid.Parse(appUser.Id);

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedById = userId;
                        entry.Entity.Created = dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedById = userId;
                        entry.Entity.LastModified = dateTime.Now;
                        break;
                }
            }

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<User> entry in ChangeTracker.Entries<User>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedById = userId;
                        entry.Entity.Created = dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedById = userId;
                        entry.Entity.LastModified = dateTime.Now;
                        break;
                }
            }

            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        #region Entities

        public DbSet<CustomerOrganization> CustomerOrganizations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupType> GroupTypes { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<IssueUrl> IssueUrls { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketAttachment> TicketAttachment { get; set; }
        public DbSet<TicketIssue> TicketIssues { get; set; }
        //public DbSet<TicketManagingPipeline> TicketManagingPipelines { get; set; }
        public DbSet<TicketStatus> TicketStatus { get; set; }
        public DbSet<UserIssueUrl> UserIssueUrl { get; set; }
        #endregion
    }
}
