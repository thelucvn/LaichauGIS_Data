namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
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
            Photos = new HashSet<Photo>();
            WarningSettings = new HashSet<WarningSetting>();
        }

        [Key]
        public int mLocationID { get; set; }

        [Required]
        [StringLength(50)]
        public string mLocationName { get; set; }

        public int supplierID { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public int wardID { get; set; }

        public int mLocationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeasurementData> MeasurementDatas { get; set; }

        public virtual UserAccount UserAccount { get; set; }

        public virtual Ward Ward { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Photo> Photos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WarningSetting> WarningSettings { get; set; }
    }
}
