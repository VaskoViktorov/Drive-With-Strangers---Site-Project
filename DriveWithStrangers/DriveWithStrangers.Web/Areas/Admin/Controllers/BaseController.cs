namespace DriveWithStrangers.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    [Area(AdminAreaName)]
    [Authorize(Roles = AdministratorRole)]
    public abstract class BaseController : Controller
    {
       
    }
}
