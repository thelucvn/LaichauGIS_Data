namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WarningSetting")]
    public partial class WarningSetting
    {
        [Key]
        public int settingID { get; set; }

        public int userID { get; set; }
        [DisplayName("Mức cảnh báo")]
        public int levelSetting { get; set; }
        [DisplayName("Trạng thái")]
        public int settingStatus { get; set; }
        [DisplayName("Ngày thiết lập")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime settingDate { get; set; }

        public int locationID { get; set; }

        public int dataTypeID { get; set; }

        public virtual DataType DataType { get; set; }

        public virtual MeasurementLocation MeasurementLocation { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}
