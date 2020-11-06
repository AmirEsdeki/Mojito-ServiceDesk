using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mojito.ServiceDesk.Application.Common.Interfaces;
using Mojito.ServiceDesk.Application.Common.Interfaces.Common;
using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using Mojito.ServiceDesk.Core.Entities.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Data.EF
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        private readonly IDateTimeService dateTime;
        private readonly IAuthenticationService authenticationService;

        public ApplicationDBContext(DbContextOptions options, IDateTimeService dateTime, IAuthenticationService authenticationService) : base(options)
        {
            this.dateTime = dateTime;
            this.authenticationService = authenticationService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Configuration
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
            builder.HasDefaultSchema("identity");
            base.OnModelCreating(builder);
            #endregion

            #region SeedData
            #endregion
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var userId = authenticationService?.Identity?.UserId;

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
        //public DbSet<User> Users { get; set; }
        //public DbSet<Group> Groups { get; set; }
        //public DbSet<GroupType> GroupTypes { get; set; }
        //public DbSet<ProfileImage> ProfileImages { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<Post> Posts { get; set; }
        //public DbSet<UserGroup> UserGroup { get; set; }
        //public DbSet<Conversation> Conversations { get; set; }
        //public DbSet<IssueUrl> IssueUrls { get; set; }
        //public DbSet<Priority> Priorities { get; set; }
        //public DbSet<Ticket> Tickets { get; set; }
        //public DbSet<TicketAttachment> TicketAttachment { get; set; }
        //public DbSet<TicketIssue> TicketIssues { get; set; }
        //public DbSet<TicketManagingPipeline> TicketManagingPipelines { get; set; }
        //public DbSet<TicketStatus> TicketStatus { get; set; }
        //public DbSet<UserIssueUrl> UserIssueUrl { get; set; }
        #endregion
    }
}
