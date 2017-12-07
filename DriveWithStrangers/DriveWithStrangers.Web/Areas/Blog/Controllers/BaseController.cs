namespace DriveWithStrangers.Web.Areas.Blog.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    [Area(BlogAreaName)]
    [Authorize(Roles = BlogAuthorRole)]
    public class BaseController : Controller
    {
    }
}


