using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LaichauGIS_Data.Areas.Admin.Models;
using Models.Framework;
using PagedList;

namespace LaichauGIS_Data.Areas.Admin.Controllers
{
    [Authorize]
    public class PhotosController : MyBaseController
    {
        private LaichauDBContext db = new LaichauDBContext();
        public PhotosController()
        {
            GetMyBaseController();
        }

        // GET: Admin/Photos
        public ActionResult Index(int page=1,int pageSize=15)
        {
            var photos = ListAll(page,pageSize);
            return View(photos);
        }

        // GET: Admin/Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }
        public IEnumerable<Photo> ListAll(int page, int pageSize)
        {
            var list = db.Database.SqlQuery<Photo>("sp_Photo_ListAll").ToList().OrderByDescending(x => x.photoID).ToPagedList(page, pageSize);
            return list;
        }
        // GET: Admin/Photos/Create
        public ActionResult Create()
        {
            ViewBag.uploadBy = new SelectList(db.UserAccounts, "userID", "userName");
            ViewBag.wardID = new SelectList(db.Wards, "wardID", "wardName");
            return View();
        }

        // POST: Admin/Photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Photo photo)
        {
            if (ModelState.IsValid)
            {
              //  string fileName = Path.GetFileNameWithoutExtension(photo.ImageFile.FileName);
                string fileExtension = Path.GetExtension(photo.ImageFile.FileName);
                var uniqFileName = Guid.NewGuid().ToString();
                var fileName = Path.GetFileName(uniqFileName + fileExtension.ToLower());
                photo.photoUrl = @"\Upload\UserImages\" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Upload/UserImages/"), fileName);
                photo.ImageFile.SaveAs(fileName);

                DateTime uploadDate = DateTime.Now;
                SqlParameter[] sqlParams = {new SqlParameter("@PhotoTitle",photo.photoTitle),
                                            new SqlParameter("@WardID",photo.wardID),
                                            new SqlParameter("@UploadBy",photo.uploadBy),
                                            new SqlParameter("@UploadDate",uploadDate),
                                            new SqlParameter("@PhotoUrl",photo.photoUrl)};
                db.Database.SqlQuery<int>("exec sp_AddNewPhoto @PhotoTitle,@WardID,@UploadBy,@UploadDate,@PhotoUrl", sqlParams).FirstOrDefault();
                return RedirectToAction("Index");
            }

            ViewBag.uploadBy = new SelectList(db.UserAccounts, "userID", "userName", photo.uploadBy);
            ViewBag.wardID = new SelectList(db.Wards, "wardID", "wardName", photo.wardID);
            return View(photo);
        }

        // GET: Admin/Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            ViewBag.uploadBy = new SelectList(db.UserAccounts, "userID", "userName", photo.uploadBy);
            ViewBag.wardID = new SelectList(db.Wards, "wardID", "wardName", photo.wardID);
            return View(photo);
        }

        // POST: Admin/Photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "photoID,photoTitle,wardID,uploadBy,uploadDate,photoStatus,photoUrl")] Photo photo)
        {
            if (photo.photoTitle!=null)
            {
                SqlParameter[] sqlParams = {new SqlParameter("@PhotoTitle",photo.photoTitle),
                                            new SqlParameter("@PhotoID",photo.photoID)};
                db.Database.ExecuteSqlCommand("Update Photo set photoTitle=@PhotoTitle where photoID=@PhotoID", sqlParams);
                return RedirectToAction("Index");
            }
            ViewBag.uploadBy = new SelectList(db.UserAccounts, "userID", "userName", photo.uploadBy);
            ViewBag.wardID = new SelectList(db.Wards, "wardID", "wardName", photo.wardID);
            return View(photo);
        }

        // GET: Admin/Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Admin/Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
