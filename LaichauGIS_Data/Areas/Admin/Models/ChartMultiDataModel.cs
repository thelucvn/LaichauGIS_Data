﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaichauGIS_Data.Areas.Admin.Models
{
    public class ChartMultiDataModel
    {
        public DateTime x { get; set; }
        public Nullable<double> y1 { get; set; }
        public Nullable<double> y2 { get; set; }
        public Nullable<double> y3 { get; set; }
        public Nullable<double> y4 { get; set; }
        public Nullable<double> y5 { get; set; }
    }
}