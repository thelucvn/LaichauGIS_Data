using LaichauGIS_Data.Areas.Admin.Models;
using Models.Framework;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LaichauGIS_Data.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminHomeController : MyBaseController
    {
        MyBaseModel baseModel;
        LaichauDBContext context;
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        public AdminHomeController()
        {
            
        }

        // GET: Admin/AdminHome
        public async Task<ActionResult> Index()
        {
            context = new LaichauDBContext();
            string loginName = Request.Cookies["user"].Value;
            var loginAccount = await context.Database.SqlQuery<UserAccount>("exec sp_GetUserAccount_ByLoginName @LoginName", new SqlParameter("@LoginName", loginName)).FirstOrDefaultAsync();
            baseModel = new MyBaseModel();
            baseModel.LoginAccount = loginAccount;
            MyBaseController.GetMyBaseController().setBaseModel(baseModel);

            context = new LaichauDBContext();
            var tempData = getLastMDataByType(1);
            var airHumiData = getLastMDataByType(2);
            var soilHumiData = getLastMDataByType(3);
            var rainyData = getLastMDataByType(4);
            var locationList = context.Database.SqlQuery<string>("select mLocationName from MeasurementLocation").ToList();

            try
            {
                ViewBag.TemperatureData = JsonConvert.SerializeObject(tempData, _jsonSetting);
                ViewBag.AirHumidityData = JsonConvert.SerializeObject(airHumiData, _jsonSetting);
                ViewBag.SoilHumidityData = JsonConvert.SerializeObject(soilHumiData, _jsonSetting);
                ViewBag.RainyData = JsonConvert.SerializeObject(rainyData, _jsonSetting);
                ViewBag.MLocation = JsonConvert.SerializeObject(locationList, _jsonSetting);
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

        List<ChartMultiDataModel> getLastMDataByType(int dataTypeID)
        {
            context = new LaichauDBContext();
            var res = context.Database.SqlQuery<ChartMultiDataModel>("exec sp_GetLastMDataByType @DataTypeID", new SqlParameter("@DataTypeID",dataTypeID)).ToList();
            return res;
        }
        [HttpGet]
        public ActionResult NotFoundPage()
        {
            return View();
        }
        [HttpGet]
        public ActionResult BlankPage()
        {
            return View();
        }

    }
}