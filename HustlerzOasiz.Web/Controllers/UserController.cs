using Microsoft.AspNetCore.Authorization;
using static HustlerzOasiz.Common.GeneralApplicationConstants;

namespace HustlerzOasiz.Web.Controllers
{
    /// <summary>
    /// only the admin can user this controller
    /// </summary>
    [Authorize(Roles = AdminRoleName)] 
    public class UserController : BaseController
    {
        
    }
}
