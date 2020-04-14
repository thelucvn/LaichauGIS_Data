using LaichauGIS_Data.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Framework;
namespace LaichauGIS_Data.Areas.Admin.Controllers
{
    public class MyBaseController : Controller
    {
        MyBaseModel baseModel;
        public static MyBaseController baseControllerInstance;
        public MyBaseModel BaseModel { get => baseModel; set => baseModel = value; }
        public MyBaseModel getBaseModel()
        {
            return baseModel;
        }
        public void setBaseModel(MyBaseModel bModel)
        {
            baseModel = bModel;
        }
        public static MyBaseController GetMyBaseController()
        {
            if (baseControllerInstance == null)
                baseControllerInstance = new MyBaseController();
            return baseControllerInstance;
        }
        [AcceptVerbs(HttpVerbs.Post|HttpVerbs.Get)]
        public string GetName()
        {
            MyBaseModel mBaseModel = baseControllerInstance.getBaseModel();
            if (mBaseModel == null)
                return "";
            return mBaseModel.LoginAccount.userName;
        }
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public string GetUserPhotoUrl()
        {
            MyBaseModel mBaseModel = baseControllerInstance.getBaseModel();
            if (mBaseModel == null)
                return "";
            return mBaseModel.LoginAccount.userPhoto;
        }
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public int GetUserID()
        {
            MyBaseModel mBaseModel = baseControllerInstance.getBaseModel();
            if (mBaseModel == null)
                return 0;
            return mBaseModel.LoginAccount.userID;
        }
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public string GetEditAccount()
        {          
            string href = "Admin/UserAccount/Edit/" + baseControllerInstance.GetUserID().ToString();
            return href;
        }
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public string GetDetailsAccount()
        {
            string href = "Admin/UserAccount/Details/" + baseControllerInstance.GetUserID().ToString();
            return href;
        }
    }
}