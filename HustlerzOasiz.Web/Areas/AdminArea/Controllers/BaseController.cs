using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HustlerzOasiz.Common.GeneralApplicationConstants;

namespace HustlerzOasiz.Web.Areas.AdminArea.Controllers
{
    [Authorize(Roles = AdminRoleName)]
    [Area(AdminAreaName)]
    public class BaseController : Controller
    {
      
    }
}
