using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaichauGIS_Data.Areas.Admin.Models
{
    public class DataSummary
    {
        public int dataIndex { get; set; }
        public string mLocationName { get; set; }
       public Nullable<double> lastMeasuredData { get; set; }
        public Nullable<double> averageInWeek { get; set; }
        public Nullable<double> averageInMonth { get; set; }
    }
}