﻿using Models;
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

        // GET: Admin/UserAccount/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
            return View();
        }

        // POST: Admin/UserAccount/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/UserAccount/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/UserAccount/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}