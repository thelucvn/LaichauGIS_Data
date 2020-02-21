namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ward")]
    public partial class Ward
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ward()
        {
            MeasurementLocations = new HashSet<MeasurementLocation>();
        }
        public Ward(int id)
        {
            this.districtID = id;
        }
        public int wardID { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Tên xã")]
        public string wardName { get; set; }

        [DisplayName("Vĩ độ")]
        public double? latitude { get; set; }
        [DisplayName("Kinh độ")]
        public double? longitude { get; set; }

        public int districtID { get; set; }
        [DisplayName("Diện tích (Km2)")]
        public double? area { get; set; }
        [DisplayName("Dân số")]
        public int? population { get; set; }
        [DisplayName("Thông tin mô tả")]
        public string wardDescription { get; set; }

        public virtual District District { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeasurementLocation> MeasurementLocations { get; set; }
    }
}
