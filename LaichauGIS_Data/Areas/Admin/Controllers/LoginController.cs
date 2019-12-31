using LaichauGIS_Data.Areas.Admin.Code;
using LaichauGIS_Data.Areas.Admin.Models;
using Models;
using System.Web.Mvc;
using System.Web.Security;

namespace LaichauGIS_Data.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
           // var result = new UserAccountModel().Login(model.LoginName, model.LoginPassword);
            if (Membership.ValidateUser(model.LoginName,model.LoginPassword) && ModelState.IsValid)
            {
                //SessionHelper.SetSession(new UserSession() { LoginName = model.LoginName });
                FormsAuthentication.SetAuthCookie(model.LoginName, model.RememberMe);
                return RedirectToAction("Index", "AdminHome");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng !");
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}