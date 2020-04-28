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
       public double lastMeasuredData { get; set; }
        public double averageInWeek { get; set; }
        public double averageInMonth { get; set; }
    }
}