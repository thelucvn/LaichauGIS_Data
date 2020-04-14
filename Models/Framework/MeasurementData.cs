namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MeasurementData")]
    public partial class MeasurementData
    {
        [Key]
        [Column(Order = 1)]
        public int mDataID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int mLocationID { get; set; }

        [DisplayName("Thời điểm cập nhật")]
        public DateTime updateTime { get; set; }
        [Key]
        [Column(Order = 3)]
        public int dataTypeID { get; set; }

        [DisplayName("Giá trị")]
        public double mDataValue { get; set; }
       
        [DisplayName("Dữ liệu đo")]
        public string dataTypeName { get; set; }
        [DisplayName("Vị trí điểm đo")]
        public string mLocationName { get; set; }
        [DisplayName("Đơn vị")]
        public string mUnit { get; set; }

    }
}
