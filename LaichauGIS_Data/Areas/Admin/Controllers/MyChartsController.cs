using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Framework;
using Newtonsoft.Json;
using LaichauGIS_Data.Areas.Admin.Models;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.IO;

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
			var tempData = getTemperatureData(id);
			var airHumiData = getAirHumidityData(id);
			var soilHumiData = getSoilHumidityData(id);
			var rainyData = getRainyData(id);
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
		List<MyChartModel> getTemperatureData(int? id)
		{
			_context = new LaichauDBContext();
			var res=_context.Database.SqlQuery<MyChartModel>("exec sp_GetRecentDataChart @DataTypeID,@LocationID", GetSqlParameters(id, 1)).ToList();
			return res;
		}
		List<MyChartModel> getAirHumidityData(int? id)
		{
			_context = new LaichauDBContext();
			var res = _context.Database.SqlQuery<MyChartModel>("exec sp_GetRecentDataChart @DataTypeID,@LocationID", GetSqlParameters(id, 2)).ToList();
			return res;
		}
		List<MyChartModel> getSoilHumidityData(int? id)
		{
			_context = new LaichauDBContext();
			var res = _context.Database.SqlQuery<MyChartModel>("exec sp_GetRecentDataChart @DataTypeID,@LocationID", GetSqlParameters(id, 3)).ToList();
			return res;
		}
		List<MyChartModel> getRainyData(int? id)
		{
			_context = new LaichauDBContext();
			var res= _context.Database.SqlQuery<MyChartModel>("exec sp_GetRecentDataChart @DataTypeID,@LocationID", GetSqlParameters(id, 4)).ToList();
			return res;
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
		public ActionResult Report(string id)
		{
			LocalReport lr = new LocalReport();
			string path = Path.Combine(Server.MapPath("~/Report"), "ReportChartDataMeasure.rdlc");
			if (System.IO.File.Exists(path))
			{
				lr.ReportPath = path;
			}
			else
			{
				return View();
			}
			List<MyChartModel> nhietdo = getTemperatureData(1);
			List<MyChartModel> doamkk = getAirHumidityData(2);
			List<MyChartModel> doamdat = getSoilHumidityData(3);
			List<MyChartModel> luongmua = getRainyData(4);
			ReportDataSource rdNhietDo = new ReportDataSource("Nhietdo", nhietdo);
			ReportDataSource rdDoamkk = new ReportDataSource("Doamkk", doamkk);
			ReportDataSource rdDoamdat= new ReportDataSource("Doamdat", doamdat);
			ReportDataSource rdLuongmua = new ReportDataSource("Luongmua", luongmua);
			lr.DataSources.Add(rdNhietDo);
			lr.DataSources.Add(rdDoamkk);
			lr.DataSources.Add(rdDoamdat);
			lr.DataSources.Add(rdLuongmua);

			string reportType = id;
			string mimeType;
			string encoding;
			string fileNameExtension;
			string deviceInfo =
				"<DeviceInfo>" +
				"<OutputFormat>" + id + "</OutputFormat>" +
				"<PageWidth>11in</PageWidth>" +
				"<PageHeight>8.5in</PageHeight>" +
				"<MarginTop>0.5in</MarginTop>" +
				"<MarginLeft>1in</MarginLeft>" +
				"<MarginRight>1in</MarginRight>" +
				"<MarginBottom>0.5in</MarginBottom>" +
				"</DeviceInfo>";
			Warning[] warnings;
			string[] streams;
			byte[] renderedBytes;

			renderedBytes = lr.Render(
				reportType,
				deviceInfo,
				out mimeType,
				out encoding,
				out fileNameExtension,
				out streams,
				out warnings
				);
			return File(renderedBytes, mimeType);
		}
    }
}