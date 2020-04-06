using LaichauGIS_Data.Areas.Admin.Models;
using Models.Framework;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LaichauGIS_Data.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminHomeController : MyBaseController
    {
        MyBaseModel baseModel;
        LaichauDBContext context;
        public AdminHomeController()
        {
            
        }
        // GET: Admin/AdminHome
        public async Task<ActionResult> Index()
        {
            context = new LaichauDBContext();
            string loginName= Request.Cookies["user"].Value;
            var loginAccount = await context.Database.SqlQuery<UserAccount>("exec sp_GetUserAccount_ByLoginName @LoginName", new SqlParameter("@LoginName", loginName)).FirstOrDefaultAsync();
            baseModel = new MyBaseModel();
            baseModel.LoginAccount = loginAccount;
            MyBaseController.GetMyBaseController().setBaseModel(baseModel);
            return View();
        }
    }
}