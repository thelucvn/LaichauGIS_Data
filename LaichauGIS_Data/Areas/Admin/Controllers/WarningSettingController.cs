using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.Framework;

namespace LaichauGIS_Data.Areas.Admin.Controllers
{
    [Authorize]
    public class WarningSettingController : MyBaseController
    {
        private LaichauDBContext db = new LaichauDBContext();
        public WarningSettingController()
        {
            MyBaseController.GetMyBaseController();
        }
        // GET: Admin/WarningSetting
        public ActionResult Index()
        {
            var warningSettings = db.WarningSettings.Include(w => w.DataType).Include(w => w.MeasurementLocation).Include(w => w.UserAccount).OrderByDescending(w=>w.settingID);
            return View(warningSettings.ToList());
        }

        // GET: Admin/WarningSetting/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WarningSetting warningSetting = db.WarningSettings.Find(id);
            if (warningSetting == null)
            {
                return HttpNotFound();
            }
            return View(warningSetting);
        }

        // GET: Admin/WarningSetting/Create
        public ActionResult Create()
        {
            ViewBag.dataTypeID = new SelectList(db.DataTypes, "dataTypeID", "dataTypeName");
            ViewBag.locationID = new SelectList(db.MeasurementLocations, "mLocationID", "mLocationName");
            ViewBag.userID = new SelectList(db.UserAccounts, "userID", "userName");
            return View();
        }

        // POST: Admin/WarningSetting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "settingID,userID,levelSetting,settingStatus,settingDate,locationID,dataTypeID")] WarningSetting warningSetting)
        {
            if (ModelState.IsValid)
            {
                warningSetting.settingDate = DateTime.Now;
                db.WarningSettings.Add(warningSetting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.dataTypeID = new SelectList(db.DataTypes, "dataTypeID", "dataTypeName", warningSetting.dataTypeID);
            ViewBag.locationID = new SelectList(db.MeasurementLocations, "mLocationID", "mLocationName", warningSetting.locationID);
            ViewBag.userID = new SelectList(db.UserAccounts, "userID", "userName", warningSetting.userID);
            return View(warningSetting);
        }

        // GET: Admin/WarningSetting/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WarningSetting warningSetting = db.WarningSettings.Find(id);
            if (warningSetting == null)
            {
                return HttpNotFound();
            }
            ViewBag.dataTypeID = new SelectList(db.DataTypes, "dataTypeID", "dataTypeName", warningSetting.dataTypeID);
            ViewBag.locationID = new SelectList(db.MeasurementLocations, "mLocationID", "mLocationName", warningSetting.locationID);
            ViewBag.userID = new SelectList(db.UserAccounts, "userID", "userName", warningSetting.userID);
            return View(warningSetting);
        }

        // POST: Admin/WarningSetting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "settingID,userID,levelSetting,settingStatus,settingDate,locationID,dataTypeID")] WarningSetting warningSetting)
        {
            if (ModelState.IsValid)
            {
                warningSetting.settingDate = DateTime.Now;
                db.Entry(warningSetting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dataTypeID = new SelectList(db.DataTypes, "dataTypeID", "dataTypeName", warningSetting.dataTypeID);
            ViewBag.locationID = new SelectList(db.MeasurementLocations, "mLocationID", "mLocationName", warningSetting.locationID);
            ViewBag.userID = new SelectList(db.UserAccounts, "userID", "userName", warningSetting.userID);
            return View(warningSetting);
        }

        // GET: Admin/WarningSetting/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WarningSetting warningSetting = db.WarningSettings.Find(id);
            if (warningSetting == null)
            {
                return HttpNotFound();
            }
            return View(warningSetting);
        }

        // POST: Admin/WarningSetting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WarningSetting warningSetting = db.WarningSettings.Find(id);
            db.WarningSettings.Remove(warningSetting);
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
