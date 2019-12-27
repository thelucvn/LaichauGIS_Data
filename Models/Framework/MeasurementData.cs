namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MeasurementData")]
    public partial class MeasurementData
    {
        [Key]
        public int mDataID { get; set; }

        public int mLocationID { get; set; }

        public DateTime updateTime { get; set; }

        public int dataTypeID { get; set; }

        public int supplierID { get; set; }

        public double mDataValue { get; set; }

        public virtual DataType DataType { get; set; }

        public virtual MeasurementLocation MeasurementLocation { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}
