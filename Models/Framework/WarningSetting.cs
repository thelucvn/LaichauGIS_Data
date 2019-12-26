namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WarningSetting")]
    public partial class WarningSetting
    {
        [Key]
        public int settingID { get; set; }

        public int userID { get; set; }

        public int levelSetting { get; set; }

        public int settingStatus { get; set; }

        public DateTime settingDate { get; set; }

        public int locationID { get; set; }

        public int dataTypeID { get; set; }

        public virtual DataType DataType { get; set; }

        public virtual MeasurementLocation MeasurementLocation { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}
