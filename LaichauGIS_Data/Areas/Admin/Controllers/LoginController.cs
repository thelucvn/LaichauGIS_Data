using LaichauGIS_Data.Areas.Admin.Code;
using LaichauGIS_Data.Areas.Admin.Models;
using Models;
using System.Web.Mvc;
using System.Web.Security;
using Models.Framework;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System;

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
            if (model.LoginName!=null && model.LoginPassword!=null && Membership.ValidateUser(model.LoginName,model.LoginPassword) && ModelState.IsValid)
            {
                //SessionHelper.SetSession(new UserSession() { LoginName = model.LoginName });
                FormsAuthentication.SetAuthCookie(model.LoginName, model.RememberMe);
                Response.Cookies.Add(new System.Web.HttpCookie("user", model.LoginName));
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
        //GET: Admin/Login/ResetPassword
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
        //Post: Admin/Login/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(string emailAddress)
        {
            if (emailAddress == "")
            {
                ModelState.AddModelError("", "Địa chỉ email không hợp lệ!");
                return View();
            }
            if (ModelState.IsValid)
            {
                LaichauDBContext context = new LaichauDBContext();
                int res=await context.Database.SqlQuery<int>("select count(*) from UserAccount where emailAddress=@EmailAddress", new SqlParameter("@EmailAddress", emailAddress)).CountAsync();
                if (res > 0)
                {
                   var resetPassword= await sendEmailResetPassword(emailAddress);
                    return RedirectToAction("Index", "Login");
                }
            }
            return View();
        }
       async Task<bool> sendEmailResetPassword(string emailAddress)
        {
            var resetPass= Guid.NewGuid().ToString("d").Substring(1, 8);
            LaichauDBContext context = new LaichauDBContext();
            var loginName = await context.Database.SqlQuery<string>("Select loginName from UserAccount where emailAddress=@EmailAddress", new SqlParameter("@EmailAddress", emailAddress)).SingleOrDefaultAsync();
            SqlParameter[] sqlParams = { new SqlParameter("@NewPassword", resetPass), new SqlParameter("@EmailAddress", emailAddress) };
            var res = context.Database.ExecuteSqlCommand("Update UserAccount set loginPassword=@NewPassword where emailAddress=@EmailAddress", sqlParams);
            if (res > 0)
            {
                var senderEmail = new MailAddress("ct0106.sys@gmail.com", "Admin email");
                var receiverEmail = new MailAddress(emailAddress, "Receiver");
                var password = "Humgct@0106";
                var sub = "Khôi phục mật khẩu";
                var body = "Đây là email khôi phục mật khẩu đề nghị không trả lời email này.\nTên đăng nhập: "+loginName+"\nMật khẩu mới của bạn: " + resetPass;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                    return true;
                }
            }
            return false;

        }

    }
}