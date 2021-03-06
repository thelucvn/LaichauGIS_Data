﻿using Models;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using System.IO;
using System.Configuration;

namespace LaichauGIS_Data.Areas.Admin.Controllers
{
    [Authorize]
    public class UserAccountController : MyBaseController
    {
        public UserAccountController()
        {
            MyBaseController.GetMyBaseController();
        }
        // GET: Admin/UserAccount
        public ActionResult Index(int page=1,int pageSize=15)
        {
            var iplUserAccount = new UserAccountModel();
            var model = iplUserAccount.ListAll(page,pageSize);
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
            if (model.roleID != 2)
            {
                string serverMapPath = ConfigurationManager.AppSettings["BaseAddress_2"];
                model.userPhoto = serverMapPath + model.userPhoto;
            }
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
            if (userEntity.roleID != 2)
            {
                string serverMapPath = ConfigurationManager.AppSettings["BaseAddress_2"];
                userEntity.userPhoto = serverMapPath + userEntity.userPhoto;
            }
            return View(userEntity);
        }

        // POST: Admin/UserAccount/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserAccount collection)
        {
            // TODO: Add update logic here
            if (collection.ImageFile != null)
            {
                string fileExtension = Path.GetExtension(collection.ImageFile.FileName);
                var uniqFileName = Guid.NewGuid().ToString();
                var fileName = Path.GetFileName(uniqFileName + fileExtension.ToLower());
                collection.userPhoto = @"\Upload\UserImages\" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Upload/UserImages/"), fileName);
                collection.ImageFile.SaveAs(fileName);
            }
            collection.birthDate = collection.SelectedDate;
            string baseAddress = ConfigurationManager.AppSettings["BaseAddress_2"];
            collection.userPhoto = collection.userPhoto.Replace(baseAddress, "");
                    UserAccountModel model = new UserAccountModel();
                    bool res = model.UpdateUserAccount(collection);
            if (collection.roleID == 3)
            {
                return RedirectToAction("ManageProvider");
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
