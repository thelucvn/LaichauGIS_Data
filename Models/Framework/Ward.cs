namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
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

        public int wardID { get; set; }

        [Required]
        [StringLength(50)]
        public string wardName { get; set; }

        public double? latitude { get; set; }

        public double? longitude { get; set; }

        public int districtID { get; set; }

        public double? area { get; set; }

        public int? population { get; set; }

        public string wardDescription { get; set; }

        public virtual District District { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeasurementLocation> MeasurementLocations { get; set; }
    }
}
