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
  
            var measurementDatas = db.Database.SqlQuery<MeasurementData>("exec sp_GetAllMeasurementData").ToList().OrderByDescending(x => x.mDataID).ToPagedList(page, pageSize);
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
