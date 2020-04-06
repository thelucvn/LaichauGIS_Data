using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaichauGIS_Data.Areas.Admin.Models
{
    public class MyBaseModel
    {
        UserAccount loginAccount;

        public UserAccount LoginAccount { get => loginAccount; set => loginAccount = value; }
       
    }
}