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
    public class MeasurementLocationController : MyBaseController
    {
        private LaichauDBContext db = new LaichauDBContext();

        public MeasurementLocationController()
        {
            MyBaseController.GetMyBaseController();
        }
        // GET: Admin/MeasurementLocation
        public ActionResult Index()
        {
            var measurementLocations = db.MeasurementLocations.Include(m => m.UserAccount).Include(m => m.Ward);
            return View(measurementLocations.ToList());
        }

        // GET: Admin/MeasurementLocation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasurementLocation measurementLocation = db.MeasurementLocations.Find(id);
            if (measurementLocation == null)
            {
                return HttpNotFound();
            }
            return View(measurementLocation);
        }

        // GET: Admin/MeasurementLocation/Create
        public ActionResult Create()
        {
            ViewBag.supplierID = new SelectList(db.UserAccounts.Where(e => e.roleID == 3),"userID","userName");
            ViewBag.wardID = new SelectList(db.Wards, "wardID", "wardName");
            return View();
        }

        // POST: Admin/MeasurementLocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mLocationID,mLocationName,supplierID,Longitude,Latitude,wardID,mLocationStatus")] MeasurementLocation measurementLocation)
        {
            if (ModelState.IsValid)
            {
                db.MeasurementLocations.Add(measurementLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.supplierID = new SelectList(db.UserAccounts.Where(e=>e.roleID==3), "userID", "userName", measurementLocation.supplierID);
            ViewBag.wardID = new SelectList(db.Wards, "wardID", "wardName", measurementLocation.wardID);
            return View(measurementLocation);
        }

        // GET: Admin/MeasurementLocation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasurementLocation measurementLocation = db.MeasurementLocations.Find(id);
            if (measurementLocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.supplierID = new SelectList(db.UserAccounts.Where(e=>e.roleID==3), "userID", "userName", measurementLocation.supplierID);
            ViewBag.wardID = new SelectList(db.Wards, "wardID", "wardName", measurementLocation.wardID);
            return View(measurementLocation);
        }

        // POST: Admin/MeasurementLocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mLocationID,mLocationName,supplierID,Longitude,Latitude,wardID,mLocationStatus")] MeasurementLocation measurementLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(measurementLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.supplierID = new SelectList(db.UserAccounts.Where(e=>e.roleID==3), "userID", "userName", measurementLocation.supplierID);
            ViewBag.wardID = new SelectList(db.Wards, "wardID", "wardName", measurementLocation.wardID);
            return View(measurementLocation);
        }

        // GET: Admin/MeasurementLocation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasurementLocation measurementLocation = db.MeasurementLocations.Find(id);
            if (measurementLocation == null)
            {
                return HttpNotFound();
            }
            return View(measurementLocation);
        }

        // POST: Admin/MeasurementLocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MeasurementLocation measurementLocation = db.MeasurementLocations.Find(id);
            db.MeasurementLocations.Remove(measurementLocation);
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
