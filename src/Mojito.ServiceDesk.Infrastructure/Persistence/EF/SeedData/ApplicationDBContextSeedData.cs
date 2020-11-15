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
                    new GroupType{Title = "دپارتمان" },
                    new GroupType{Title = "پشتیبانی" },
                    new GroupType{Title = "مالی"},
                    new GroupType{Title = "اداری"},
                    new GroupType{Title = "عملیات"},
                });

                await context.SaveChangesAsync();
            }

            if (!context.Groups.Any())
            {
                context.Groups.AddRange(new List<Group>()
                {
                    new Group{Name = "ریسک" , groupType = context.GroupTypes.FirstOrDefault(f => f.Title=="دپارتمان") },
                    new Group{Name = "پشتیبانی ریسک" , groupType = context.GroupTypes.FirstOrDefault(f => f.Title=="پشتیبانی") },
                    new Group{Name = "پرداخت" , groupType = context.GroupTypes.FirstOrDefault(f => f.Title=="دپارتمان") },
                    new Group{Name = "پشتیبانی پرداخت" , groupType = context.GroupTypes.FirstOrDefault(f => f.Title=="پشتیبانی") },
                    new Group{Name = "بیمه رازی" , groupType = context.GroupTypes.FirstOrDefault(f => f.Title=="دپارتمان")},
                    new Group{Name = "پشتیبانی بیمه رازی" , groupType = context.GroupTypes.FirstOrDefault(f => f.Title=="پشتیبانی")},
                    new Group{Name = "پشتیبانی لایه اول" , groupType = context.GroupTypes.FirstOrDefault(f => f.Title=="پشتیبانی")},
                    new Group{Name = "پشتیبانی شیفت" , groupType = context.GroupTypes.FirstOrDefault(f => f.Title=="پشتیبانی")},
                });

                await context.SaveChangesAsync();
            }

            if (!context.CustomerOrganizations.Any())
            {
                context.CustomerOrganizations.AddRange(new List<CustomerOrganization>()
                {
                    new CustomerOrganization {Name = "بانک آینده" },
                    new CustomerOrganization {Name = "بانک ملت" },
                    new CustomerOrganization {Name = "بانک صنعت و معدن" },
                    new CustomerOrganization {Name = "بیمه رازی" },

                });

                await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(new List<Product>()
                {
                    new Product {Name = "سامانه جامع بانک آینده", Customer = context.CustomerOrganizations.FirstOrDefault(f => f.Name == "بانک آینده") },
                    new Product {Name = "سامانه جامع بانک ملت", Customer = context.CustomerOrganizations.FirstOrDefault(f => f.Name == "بانک ملت"  ) },
                    new Product {Name = "سامانه مدیریت فرآیندها", Customer = context.CustomerOrganizations.FirstOrDefault(f => f.Name == "بانک صنعت و معدن" ) },
                    new Product {Name = "سامانه همراز", Customer = context.CustomerOrganizations.FirstOrDefault(f => f.Name == "بیمه رازی"  ) },

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
                    new IssueUrl {Url = "bpms.ir" , Group = context.Groups.FirstOrDefault(),
                        Product = context.Products.FirstOrDefault(f => f.Name == "سامانه مدیریت فرآیندها") },
                    new IssueUrl {Url = "hamraz.ir" , Group = context.Groups.FirstOrDefault(),
                        Product = context.Products.FirstOrDefault(f => f.Name == "سامانه همراز") },
                });

                await context.SaveChangesAsync();
            }


            if (!context.Posts.Any())
            {
                context.Posts.AddRange(new List<Post>()
                {
                    new Post {Title = "برنامه نویس لایه اول"},
                    new Post {Title = "برنامه نویس لایه دوم"},
                    new Post {Title = "سرپرست فنی" },
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
                    user.IsCompanyMember = true;

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

            if (!context.TicketIssues.Any())
            {
                context.TicketIssues.AddRange(new List<TicketIssue>()
                {
                    new TicketIssue {Title = "خطا در سامانه"},
                    new TicketIssue {Title = "قطعی سامانه"},
                    new TicketIssue {Title = "درخواست اهدای دسترسی" },
                    new TicketIssue {Title = "درخواست توسعه" },
                    new TicketIssue {Title = "درخواست راهنمایی" },
                    new TicketIssue {Title = "درخواست ارائه پروپوزال" },
                    new TicketIssue {Title = "درخواست ارائه مستندات" },
                    new TicketIssue {Title = "درخواست مرخصی" , IsIntraOrganizational= true},
                    new TicketIssue {Title = "درخواست فیش حقوقی"  , IsIntraOrganizational= true},
                    new TicketIssue {Title = "درخواست گواهی اشتغال به کار"  , IsIntraOrganizational= true},
                    new TicketIssue {Title = "درخواست مساعده"  , IsIntraOrganizational= true},

                });

                await context.SaveChangesAsync();
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
