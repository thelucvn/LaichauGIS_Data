namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MeasurementLocation")]
    public partial class MeasurementLocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MeasurementLocation()
        {
            MeasurementDatas = new HashSet<MeasurementData>();
            WarningSettings = new HashSet<WarningSetting>();
        }

        [Key]
        public int mLocationID { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Tên điểm đo")]
        public string mLocationName { get; set; }

        [DisplayName("Nhà cung cấp")]
        public int supplierID { get; set; }
        [DisplayName("Vĩ độ")]
        public double? Longitude { get; set; }

        [DisplayName("Kinh độ")]
        public double? Latitude { get; set; }
        [DisplayName("Tên xã")]
        public int wardID { get; set; }

        [DisplayName("Trạng thái hoạt động")]
        public int mLocationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeasurementData> MeasurementDatas { get; set; }

        public virtual UserAccount UserAccount { get; set; }

        public virtual Ward Ward { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WarningSetting> WarningSettings { get; set; }
    }
}
