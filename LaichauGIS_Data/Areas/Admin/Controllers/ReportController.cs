using LaichauGIS_Data.Areas.Admin.Models;
using Microsoft.Reporting.WebForms;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaichauGIS_Data.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        LaichauDBContext mContext;
        List<DataSummary> getDataSummary()
        {
            mContext = new LaichauDBContext();
            var locations = mContext.Database.SqlQuery<MeasurementLocation>("exec sp_MeasurementLocation_GetData").ToList();
            List<DataSummary> dataSummaryList = new List<DataSummary>();

            foreach (MeasurementLocation mLocation in locations)
            {
                var sumaryData = mContext.Database.SqlQuery<DataSummary>("exec sp_GetDataSummary @MLocationID,@DataTypeID", GetSqlParameters(mLocation.mLocationID, 1)).FirstOrDefault();
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
            return dataSummaryList;
        }
        SqlParameter[] GetSqlParameters(int mLocationID, int dataTypeID)
        {
            SqlParameter[] sqlParams = { new SqlParameter("@MLocationID", mLocationID), new SqlParameter("@DataTypeID", dataTypeID) };
            return sqlParams;
        }
        // GET: Admin/Report
        public ActionResult Index(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report"), "ReportSummaryDataMeasure.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View();
            }
            List<DataSummary> dataSummaryList = getDataSummary();
            List<DataSummary> nhietdo = new List<DataSummary>();
            List<DataSummary> doamkk = new List<DataSummary>();
            List<DataSummary> doamdat = new List<DataSummary>();
            List<DataSummary> luongmua = new List<DataSummary>();
            for (int i = 0; i < dataSummaryList.Count; i++)
            {
                if (i < 5)
                {
                    nhietdo.Add(dataSummaryList[i]);
                }
                else if (i < 10)
                {
                    doamkk.Add(dataSummaryList[i]);
                }
                else if (i < 15)
                {
                    doamdat.Add(dataSummaryList[i]);
                }
                else
                {
                    luongmua.Add(dataSummaryList[i]);
                }
            }

            ReportDataSource rdNhietDo = new ReportDataSource("NhietDo", nhietdo);
            ReportDataSource rdDoAmKK = new ReportDataSource("DoAmKK", doamkk);
            ReportDataSource rdDoAmDat = new ReportDataSource("DoAmDat", doamdat);
            ReportDataSource rdLuongMua = new ReportDataSource("LuongMua", luongmua);
            lr.DataSources.Add(rdNhietDo);
            lr.DataSources.Add(rdDoAmKK);
            lr.DataSources.Add(rdDoAmDat);
            lr.DataSources.Add(rdLuongMua);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "<DeviceInfo>" +
                "<OutputFormat>" + id + "</OutputFormat>" +
                "<PageWidth>8.5in</PageWidth>" +
                "<PageHeight>11in</PageHeight>" +
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