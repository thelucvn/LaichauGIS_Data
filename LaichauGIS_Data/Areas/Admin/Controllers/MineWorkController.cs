using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LaichauGIS_Data.Areas.Admin.Models;
namespace LaichauGIS_Data.Areas.Admin.Controllers
{
    public class MineWorkController : MyBaseController
    {
        MineWorkData mineWorkData;
        public MineWorkController()
        {
            MyBaseController.GetMyBaseController();
        }
        // GET: Admin/MineWork
        public ActionResult Index()
        {
            mineWorkData = new MineWorkData();
            mineWorkData.veloTranMax = 2.7;
            mineWorkData.veloVertMax = 1.9;
            mineWorkData.veloLongMax = 2.4;
            mineWorkData.veloVertMaxTime = 1.07;
            mineWorkData.veloLongMaxTime = 0.61;
            mineWorkData.veloTranMaxTime = 1.73;
            mineWorkData.ampVertMax = 9.85;
            mineWorkData.ampLongMax = 3.51;
            mineWorkData.ampTranMax = 8.03;
            mineWorkData.mainFreqVert = 22.7;
            mineWorkData.mainFreqLong = 27.5;
            mineWorkData.mainFreqTran = 21.3;
            mineWorkData.sumData = calculateSum();
            return View(mineWorkData);
        }
        double calculateSum()
        {
            double res;
            double sumData;
            sumData = Math.Pow(mineWorkData.veloTranMax,2) + Math.Pow(mineWorkData.veloVertMax,2) + Math.Pow(mineWorkData.veloLongMax,2);
            res = Math.Round(Math.Sqrt(sumData),2);
            return res;
        }
    }
}