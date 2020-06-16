using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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
    public class MeasurementDataController : MyBaseController
    {
        private LaichauDBContext db = new LaichauDBContext();
        IPagedList<MeasurementData> measurementDatas;
        int pageNum;
        int pageSize = 10;
        static DataFilter pageFilter;
        public MeasurementDataController()
        {
            MyBaseController.GetMyBaseController();
        }
        public void loadMeasurementData()
        {

            if (pageFilter.dataTypeID == 0 && pageFilter.locationID == 0)
            {
                measurementDatas = db.Database.SqlQuery<MeasurementData>("exec sp_GetAllMeasurementData").ToList().OrderByDescending(x => x.mDataID).ToPagedList(pageNum, pageSize);
            }
            else if (pageFilter.dataTypeID == 0)
            {
                measurementDatas = db.Database.SqlQuery<MeasurementData>("exec sp_GetMeasurementDataByLocation @LocationID", new SqlParameter("@LocationID", pageFilter.locationID)).ToList().OrderByDescending(x => x.mDataID).ToPagedList(pageNum, pageSize);
            }
            else if (pageFilter.locationID == 0)
            {
                measurementDatas = db.Database.SqlQuery<MeasurementData>("exec sp_GetMeasurementDataByDataType @DataTypeID", new SqlParameter("@DataTypeID", pageFilter.dataTypeID)).ToList().OrderByDescending(x => x.mDataID).ToPagedList(pageNum, pageSize);
            }
            else
            {
                SqlParameter[] sqlParams = { new SqlParameter("@LocationID", pageFilter.locationID), new SqlParameter("@DataTypeID", pageFilter.dataTypeID) };
                measurementDatas = db.Database.SqlQuery<MeasurementData>("exec sp_GetMeasurementDataByLocationAndDataType @LocationID,@DataTypeID", sqlParams).ToList().OrderByDescending(x => x.mDataID).ToPagedList(pageNum, pageSize);
            }
        }
        // GET: Admin/MeasurementData
        public ActionResult Index(int page=1)
        {
            pageNum = page;
            if (pageFilter == null)
            {
                pageFilter = new DataFilter();
                pageFilter.locationID = 0;
                pageFilter.dataTypeID = 0;
            }
            if (measurementDatas == null)
            loadMeasurementData();        
            ViewBag.locationID = new SelectList(GetMeasurementLocations(), "mLocationID", "mLocationName");
            ViewBag.dataTypeID = new SelectList(GetDataTypes(), "dataTypeID", "dataTypeName");
            return View(measurementDatas);
        }
        public List<MeasurementLocation> GetMeasurementLocations()
        {
            List<MeasurementLocation> mLocationList = new List<MeasurementLocation>();
            MeasurementLocation noLocation = new MeasurementLocation();
            noLocation.mLocationID = 0;
            noLocation.mLocationName = "---Lựa chọn điểm đo---";
            mLocationList.Add(noLocation);
            mLocationList.AddRange(db.MeasurementLocations.ToList());
            return mLocationList;
        }
        public List<DataType> GetDataTypes()
        {
            DataType noType = new DataType();
            noType.dataTypeID = 0;
            noType.dataTypeName = "---Lựa chọn dữ liệu đo---";
            List<DataType> listDataType = new List<DataType>();
            listDataType.Add(noType);
            listDataType.AddRange(db.DataTypes.ToList());
            return listDataType;
        }
        //POST: Admin/MeasurementData
        [HttpPost]
        public ActionResult Index(DataFilter filter, int page = 1)
        {
            pageFilter = filter;
            pageNum = page;
            loadMeasurementData();
            ViewBag.locationID = new SelectList(GetMeasurementLocations(), "mLocationID", "mLocationName");
            ViewBag.dataTypeID = new SelectList(GetDataTypes(), "dataTypeID", "dataTypeName");
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

        public ActionResult DeleteFilter()
        {
            pageFilter.dataTypeID = 0;
            pageFilter.locationID = 0;
            return RedirectToAction("Index");
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
