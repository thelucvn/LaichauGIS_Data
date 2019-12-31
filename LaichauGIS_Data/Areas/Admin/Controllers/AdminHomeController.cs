using System.Web.Mvc;

namespace LaichauGIS_Data.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminHomeController : Controller
    {
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            return View();
        }
    }
}