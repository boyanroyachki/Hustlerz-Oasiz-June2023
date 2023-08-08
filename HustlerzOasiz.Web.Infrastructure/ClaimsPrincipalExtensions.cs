using System.Security.Claims;
using static HustlerzOasiz.Common.GeneralApplicationConstants;

namespace HustlerzOasiz.Web.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        //Returns the current User's ID in string format
        public static string? GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        //Checks if the current User is admin
        public static bool IsAdmin(this ClaimsPrincipal user) 
        {
            return user.IsInRole(AdminRoleName);
        }
    }
}
