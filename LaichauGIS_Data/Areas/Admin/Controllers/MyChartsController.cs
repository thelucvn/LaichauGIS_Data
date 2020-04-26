using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Framework;
using Newtonsoft.Json;
using LaichauGIS_Data.Areas.Admin.Models;
using System.Data.SqlClient;

namespace LaichauGIS_Data.Areas.Admin.Controllers
{
	[Authorize]
    public class MyChartsController : MyBaseController
    {
        LaichauDBContext _context;
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
		public MyChartsController()
		{
			MyBaseController.GetMyBaseController();
		}
        // GET: Admin/MyCharts
		[HttpGet]
        public ActionResult Index(int? id)
        {
            _context = new LaichauDBContext();
			if (id == null)
				id = 1;
			var tempData = _context.Database.SqlQuery<MyChartModel>("exec sp_GetRecentDataChart @DataTypeID,@LocationID", GetSqlParameters(id,1)).ToList();
			var airHumiData= _context.Database.SqlQuery<MyChartModel>("exec sp_GetRecentDataChart @DataTypeID,@LocationID", GetSqlParameters(id, 2)).ToList();
			var soilHumiData = _context.Database.SqlQuery<MyChartModel>("exec sp_GetRecentDataChart @DataTypeID,@LocationID", GetSqlParameters(id, 3)).ToList();
			var rainyData = _context.Database.SqlQuery<MyChartModel>("exec sp_GetRecentDataChart @DataTypeID,@LocationID", GetSqlParameters(id, 4)).ToList();
			var mLocations = _context.Database.SqlQuery<MeasurementLocation>("exec sp_MeasurementLocation_GetData").ToList();
			try
			{
				ViewBag.TemperatureData = JsonConvert.SerializeObject(tempData, _jsonSetting);
				ViewBag.AirHumidityData= JsonConvert.SerializeObject(airHumiData, _jsonSetting);
				ViewBag.SoilHumidityData= JsonConvert.SerializeObject(soilHumiData, _jsonSetting);
				ViewBag.RainyData= JsonConvert.SerializeObject(rainyData, _jsonSetting);
				ViewBag.mLocationID = new SelectList(mLocations, "mLocationID", "mLocationName");
				ViewBag.id = id;
				return View();
			}
			catch (System.Data.Entity.Core.EntityException)
			{
				return View("Error");
			}
			catch (System.Data.SqlClient.SqlException)
			{
				return View("Error");
			}

        }
		public SqlParameter[] GetSqlParameters(int? locationID,int dataTypeID)
		{
			SqlParameter[] sqlParams = {new SqlParameter("@DataTypeID",dataTypeID),
										new SqlParameter("@LocationID",locationID)};
			return sqlParams;
		}
		[HttpPost]
		public ActionResult Index(FormCollection collection)
		{
			int localId = int.Parse(collection[0]);
			return RedirectToAction("Index",new { id =localId});
		}
    }
}