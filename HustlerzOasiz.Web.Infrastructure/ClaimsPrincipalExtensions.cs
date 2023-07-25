using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace HustlerzOasiz.Web.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
