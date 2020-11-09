using Microsoft.AspNetCore.Identity;
using Mojito.ServiceDesk.Core.Entities.Identity;
using Mojito.ServiceDesk.Core.Entities.Proprietary;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using System.Collections.Generic;
using System.IO;
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
                    new Product {Name = "سامانه جامع بانک آینده", Customer = context.CustomerOrganizations.FirstOrDefault(f => f.Name == "بانک آینده") },
                    new Product {Name = "سامانه جامع بانک ملت", Customer = context.CustomerOrganizations.FirstOrDefault(f => f.Name == "بانک ملت"  ) },

                });

                await context.SaveChangesAsync();
            }

            if (!context.IssueUrls.Any())
            {
                context.IssueUrls.AddRange(new List<IssueUrl>()
                {
                    new IssueUrl {Url = "mahtab.ir" , Group = context.Groups.FirstOrDefault(),
                        Product = context.Products.FirstOrDefault(f => f.Name == "سامانه جامع بانک آینده") },
                    new IssueUrl {Url = "samat.ir" , Group = context.Groups.FirstOrDefault(),
                        Product = context.Products.FirstOrDefault(f => f.Name == "سامانه جامع بانک ملت") },

                });

                await context.SaveChangesAsync();
            }


            if (!context.Posts.Any())
            {
                context.Posts.AddRange(new List<Post>()
                {
                    new Post {Title = "برنامه نویس لایه اول"},
                    new Post {Title = "برنامه نویس لایه دوم"},
                    new Post {Title = "مدیر فنی" },
                    new Post {Title = "مدیر محصول" },
                    new Post {Title = "حسابدار" },

                });

                await context.SaveChangesAsync();
            }

            if (!context.ProfileImages.Any())
            {
                var image = File.ReadAllBytes(@"../Mojito.ServiceDesk.Infrastructure/Assets/Image/SampleProfileImage/myAvatar.png");

                context.ProfileImages.AddRange(new List<ProfileImage>()
                {
                    new ProfileImage{Image = image}
                });

                await context.SaveChangesAsync();
            }

            if (context.Users.Any(a => a.NormalizedUserName == "TEST"))
            {
                var user = context.Users.Single(a => a.NormalizedUserName == "TEST");

                if (!user.PhoneNumberConfirmed)
                {
                    user.PhoneNumberConfirmed = true;
                    user.IsEmployee = true;

                    user.Post = context.Posts.FirstOrDefault();

                    user.ProfileImage = context.ProfileImages.FirstOrDefault();

                    var userGroups = context.Groups.Where(w => w.Name == "کارشناسان" || w.Name == "پشتیبانی شیفت").ToList()
                        .Select(group => new UserGroup() { GroupId = group.Id, UserId = user.Id }).ToList();
                    await context.Set<UserGroup>().AddRangeAsync(userGroups);

                    var userUrls = context.IssueUrls.ToList()
                        .Select(IssueUrl => new UserIssueUrl() { IssueUrlId = IssueUrl.Id, UserId = user.Id }).ToList();
                    await context.Set<UserIssueUrl>().AddRangeAsync(userUrls);

                    await context.SaveChangesAsync();
                }
            }
        }

        public static async Task SeedUserAsync(UserManager<User> userManager)
        {
            var defaultUser = new User { UserName = "test", Email = "test@test.com" };
            var defaultUserExists = !userManager.Users.Any(u => u.UserName == defaultUser.UserName);
            if (defaultUserExists)
            {
                var res = await userManager.CreateAsync(defaultUser, "Test@1");
            }
        }
    }
}
