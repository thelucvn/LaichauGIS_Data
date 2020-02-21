namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("District")]
    public partial class District
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public District()
        {
            Wards = new HashSet<Ward>();
        }

        public int districtID { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Tên huyện")]
        public string districtName { get; set; }
        [DisplayName("Vĩ độ")]
        public double? latitude { get; set; }
        [DisplayName("Kinh độ")]
        public double? longitude { get; set; }
        [DisplayName("Diện tích (Km2)")]
        public double? area { get; set; }
        [DisplayName("Dân số (Người)")]
        public int? population { get; set; }
        [DisplayName("Thông tin mô tả")]
        public string districtDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ward> Wards { get; set; }
    }
}
