using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaichauGIS_Data.Areas.Admin.Models
{
    public class MineWorkData
    {
        public double veloTranMax { get; set; }
        public double veloVertMax { get; set; }
        public double veloLongMax { get; set; }
        public double veloTranMaxTime { get; set; }
        public double veloVertMaxTime { get; set; }
        public double veloLongMaxTime { get; set; }
        public double ampTranMax { get; set; }
        public double ampVertMax { get; set; }
        public double ampLongMax { get; set; }
        public double mainFreqTran { get; set; }
        public double mainFreqVert { get; set; }
        public double mainFreqLong { get; set; }
        public double sumData { get; set; }

    }
}