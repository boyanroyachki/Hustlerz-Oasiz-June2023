using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HustlerzOasiz.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
