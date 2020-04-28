using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Framework;
using LaichauGIS_Data.Areas.Admin.Models;
using System.Data.SqlClient;

namespace LaichauGIS_Data.Areas.Admin.Controllers
{
    [Authorize]
    public class TablesController : MyBaseController
    {
        LaichauDBContext mContext;
        public TablesController()
        {
            MyBaseController.GetMyBaseController();
        }
        // GET: Admin/Tables
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Summary() {
            mContext = new LaichauDBContext();
            var locations = mContext.Database.SqlQuery<MeasurementLocation>("exec sp_MeasurementLocation_GetData").ToList();
            List<DataSummary> dataSummaryList = new List<DataSummary>();
            int k = 0;
            foreach(MeasurementLocation mLocation in locations)
            {
                var sumaryData = mContext.Database.SqlQuery<DataSummary>("exec sp_GetDataSummary @MLocationID,@DataTypeID",GetSqlParameters(mLocation.mLocationID,1)).FirstOrDefault();
                dataSummaryList.Add(sumaryData);
            }
            foreach (MeasurementLocation mLocation in locations)
            {
                var sumaryData = mContext.Database.SqlQuery<DataSummary>("exec sp_GetDataSummary @MLocationID,@DataTypeID", GetSqlParameters(mLocation.mLocationID, 2)).FirstOrDefault();
                dataSummaryList.Add(sumaryData);
            }
            foreach (MeasurementLocation mLocation in locations)
            {
                var sumaryData = mContext.Database.SqlQuery<DataSummary>("exec sp_GetDataSummary @MLocationID,@DataTypeID", GetSqlParameters(mLocation.mLocationID, 3)).FirstOrDefault();
                dataSummaryList.Add(sumaryData);
            }
            foreach (MeasurementLocation mLocation in locations)
            {
                var sumaryData = mContext.Database.SqlQuery<DataSummary>("exec sp_GetDataSummary @MLocationID,@DataTypeID", GetSqlParameters(mLocation.mLocationID, 4)).FirstOrDefault();
                dataSummaryList.Add(sumaryData);
            }
            return View(dataSummaryList);
        }
        SqlParameter[] GetSqlParameters(int mLocationID,int dataTypeID)
        {
           SqlParameter[] sqlParams= {new SqlParameter("@MLocationID",mLocationID),new SqlParameter("@DataTypeID", dataTypeID)};
            return sqlParams;
        }
    }
}