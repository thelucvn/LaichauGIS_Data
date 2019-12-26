using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaichauGIS_Data.Areas.Admin.Code
{
    public class SessionHelper
    {
        public static void SetSession(UserSession session)
        {
            HttpContext.Current.Session["loginSession"] = session;
        }
        public static UserSession getSession()
        {
            var session = HttpContext.Current.Session["loginSession"];
            if (session == null)
            {
                return null;
            }
            else
                return session as UserSession;
        }
    }
}