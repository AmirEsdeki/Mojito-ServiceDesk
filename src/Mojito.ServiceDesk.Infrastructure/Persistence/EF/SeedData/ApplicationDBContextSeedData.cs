using Microsoft.AspNetCore.Identity;
using Mojito.ServiceDesk.Core.Entities.Identity;
using Mojito.ServiceDesk.Core.Entities.Proprietary;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.SeedData
{
    public static class ApplicationDBContextSeedData
    {
        public static async Task SeedSampleDataAsync(ApplicationDBContext context)
        {
            if (!context.Groups.Any())
            {
                context.GroupTypes.AddRange(new List<GroupType>()
                {
                    new GroupType{Title = "فنی" },
                    new GroupType{Title = "حسابداری"},
                    new GroupType{Title = "اداری"},
                });

                await context.SaveChangesAsync();
            }

            if (!context.Groups.Any())
            {
                context.Groups.AddRange(new List<Group>()
                {
                    new Group{Name = "کارشناسان" , groupType = context.GroupTypes.FirstOrDefault() },
                    new Group{Name = "پشتیبانی لایه اول" , groupType = context.GroupTypes.FirstOrDefault()},
                    new Group{Name = "پشتیبانی لایه دوم" , groupType = context.GroupTypes.FirstOrDefault()},
                    new Group{Name = "پشتیبانی لایه سوم" , groupType = context.GroupTypes.FirstOrDefault()},
                    new Group{Name = "پشتیبانی شیفت" , groupType = context.GroupTypes.FirstOrDefault()},
                });

                await context.SaveChangesAsync();
            }

            if (!context.CustomerOrganizations.Any())
            {
                context.CustomerOrganizations.AddRange(new List<CustomerOrganization>()
                {
                    new CustomerOrganization {Name = "بانک آینده" },
                    new CustomerOrganization {Name = "بانک ملت" },

                });

                await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(new List<Product>()
                {
                    new Product {Name = "سامانه جامع بانک آینده", Customer = context.CustomerOrganizations.FirstOrDefault()},
                    new Product {Name = "سامانه جامع بانک ملت", Customer = context.CustomerOrganizations.LastOrDefault() },

                });

                await context.SaveChangesAsync();
            }

            if (!context.IssueUrls.Any())
            {
                context.IssueUrls.AddRange(new List<IssueUrl>()
                {
                    new IssueUrl {Url = "mahtab.ir" , Group = context.Groups.FirstOrDefault() , Product = context.Products.FirstOrDefault() },
                    new IssueUrl {Url = "samat.ir" , Group = context.Groups.FirstOrDefault() , Product = context.Products.LastOrDefault() },

                });

                await context.SaveChangesAsync();
            }

            if (!context.Users.Any(a => a.NormalizedUserName == "TEST"))
            {
                var user = context.Users.Single(a => a.NormalizedUserName == "TEST");

                user.PhoneNumberConfirmed = true;
                user.IsEmployee = true;

                user.Post = context.Posts.FirstOrDefault();

                var userGroups = context.Groups.Where(w => w.Name == "کارشناسان" || w.Name == "پشتیبانی شیفت").ToList()
                    .Select(group => new UserGroup() { GroupId = group.Id, UserId = user.Id }).ToList();
                await context.Set<UserGroup>().AddRangeAsync(userGroups);

                var userUrls = context.IssueUrls.ToList()
                    .Select(IssueUrl => new UserIssueUrl() { IssueUrlId = IssueUrl.Id, UserId = user.Id }).ToList();
                await context.Set<UserIssueUrl>().AddRangeAsync(userUrls);

                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedUserAsync(UserManager<User> userManager)
        {
            var defaultUser = new User { UserName = "test", Email = "test" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Test@1");
            }
        }
    }
}
