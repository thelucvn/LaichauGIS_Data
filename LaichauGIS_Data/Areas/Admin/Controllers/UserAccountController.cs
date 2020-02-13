using Models;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaichauGIS_Data.Areas.Admin.Controllers
{
    public class UserAccountController : Controller
    {
        // GET: Admin/UserAccount
        public ActionResult Index()
        {
            var iplUserAccount = new UserAccountModel();
            var model = iplUserAccount.ListAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult ManageProvider()
        {
            var iplUserAccount = new UserAccountModel();
            var model = iplUserAccount.ListProvider();
            return View(model);
        }
        // GET: Admin/UserAccount/Details/5
        public ActionResult Details(int id)
        {
            var iplUserAccount = new UserAccountModel();
            var model = iplUserAccount.getUserAccountByID(id);
            return View(model);
        }

        // GET: Admin/UserAccount/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/UserAccount/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserAccount collection)
        {
            
                try
                {
                    if (ModelState.IsValid)
                    {
                    UserAccountModel model = new UserAccountModel();
                    int res=model.Create(collection.userName, collection.loginName, collection.loginPassword, collection.phoneNumber, collection.emailAddress);

                        if(res>0)
                            return RedirectToAction("Index"); 
                        else
                        {                           
                            ModelState.AddModelError("", "Tạo tài khoản không thành công!");
                        }
                    }
                    return View(collection);
                }
                catch
                {
                    return View();
                }
            
        }

        // GET: Admin/UserAccount/Edit/5
        public ActionResult Edit(int id)
        {
            UserAccountModel model = new UserAccountModel();
            UserAccount userEntity = model.getUserAccountByID(id);
            return View(userEntity);
        }

        // POST: Admin/UserAccount/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserAccount collection)
        {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    UserAccountModel model = new UserAccountModel();
                    bool res = model.UpdateUserAccount(collection);
                    ModelState.AddModelError("", "Cập nhật tài khoản thành công!");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật tài khoản không thành công!");                 
                }
            return RedirectToAction("Index");

        }

        // GET: Admin/UserAccount/Delete/5
        public ActionResult Delete(int id)
        {
            var iplUserAccount = new UserAccountModel();
            var res = iplUserAccount.DeleteUserAccount(id);
            return RedirectToAction("Index");
        }

        // POST: Admin/UserAccount/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var iplUserAccount = new UserAccountModel();
                var res = iplUserAccount.DeleteUserAccount(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
