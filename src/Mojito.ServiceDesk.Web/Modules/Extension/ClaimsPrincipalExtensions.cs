using Newtonsoft.Json;
using System.Security.Claims;

namespace Mojito.ServiceDesk.Web.Modules.Extension
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetRole(this ClaimsPrincipal user)
            => user.FindFirstValue(ClaimTypes.Role);

        public static bool IsCompanyMember(this ClaimsPrincipal user)
            => bool.Parse(user.FindFirstValue("IsCompanyMember") != null ? user.FindFirstValue("IsCompanyMember") : "false");

        public static int[] GetGroups(this ClaimsPrincipal user)
        {
            if (user.FindFirstValue("Groups") != null)
                return JsonConvert.DeserializeObject<int[]>(user.FindFirstValue("Groups"));
            return null;
        }

        public static int GetCustomerOrganizationId(this ClaimsPrincipal user)
            => int.Parse(user.FindFirstValue("CustomerOrganizationId") != null && user.FindFirstValue("CustomerOrganizationId") != "" ? user.FindFirstValue("CustomerOrganizationId") : "0");
    }
}
