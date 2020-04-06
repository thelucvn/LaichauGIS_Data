using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.Framework;
using PagedList;

namespace LaichauGIS_Data.Areas.Admin.Controllers
{
    [Authorize]
    public class MeasurementDataController : MyBaseController
    {
        private LaichauDBContext db = new LaichauDBContext();
        public MeasurementDataController()
        {
            MyBaseController.GetMyBaseController();
        }

        // GET: Admin/MeasurementData
        public ActionResult Index(int page=1,int pageSize=10)
        {           
            var measurementDatas = db.MeasurementDatas.Include(m => m.DataType).Include(m => m.MeasurementLocation).ToList().OrderByDescending(x => x.mDataID).ToPagedList(page, pageSize);
            return View(measurementDatas);
        }

        // GET: Admin/MeasurementData/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasurementData measurementData = db.MeasurementDatas.Find(id);
            if (measurementData == null)
            {
                return HttpNotFound();
            }
            return View(measurementData);
        }

        // GET: Admin/MeasurementData/Create
        public ActionResult Create()
        {
            ViewBag.dataTypeID = new SelectList(db.DataTypes, "dataTypeID", "dataTypeName");
            ViewBag.mLocationID = new SelectList(db.MeasurementLocations, "mLocationID", "mLocationName");
            ViewBag.supplierID = new SelectList(db.UserAccounts, "userID", "userName");
            return View();
        }

        // POST: Admin/MeasurementData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mDataID,mLocationID,updateTime,dataTypeID,supplierID,mDataValue")] MeasurementData measurementData)
        {
            if (ModelState.IsValid)
            {
                db.MeasurementDatas.Add(measurementData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.dataTypeID = new SelectList(db.DataTypes, "dataTypeID", "dataTypeName", measurementData.dataTypeID);
            ViewBag.mLocationID = new SelectList(db.MeasurementLocations, "mLocationID", "mLocationName", measurementData.mLocationID);
            return View(measurementData);
        }

        // GET: Admin/MeasurementData/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasurementData measurementData = db.MeasurementDatas.Find(id);
            if (measurementData == null)
            {
                return HttpNotFound();
            }
            ViewBag.dataTypeID = new SelectList(db.DataTypes, "dataTypeID", "dataTypeName", measurementData.dataTypeID);
            ViewBag.mLocationID = new SelectList(db.MeasurementLocations, "mLocationID", "mLocationName", measurementData.mLocationID);
            return View(measurementData);
        }

        // POST: Admin/MeasurementData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mDataID,mLocationID,updateTime,dataTypeID,supplierID,mDataValue")] MeasurementData measurementData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(measurementData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dataTypeID = new SelectList(db.DataTypes, "dataTypeID", "dataTypeName", measurementData.dataTypeID);
            ViewBag.mLocationID = new SelectList(db.MeasurementLocations, "mLocationID", "mLocationName", measurementData.mLocationID);
            return View(measurementData);
        }

        // GET: Admin/MeasurementData/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasurementData measurementData = db.MeasurementDatas.Find(id);
            if (measurementData == null)
            {
                return HttpNotFound();
            }
            return View(measurementData);
        }

        // POST: Admin/MeasurementData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MeasurementData measurementData = db.MeasurementDatas.Find(id);
            db.MeasurementDatas.Remove(measurementData);
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
