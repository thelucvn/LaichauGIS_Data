namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserAccount")]
    public partial class UserAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserAccount()
        {
            MeasurementDatas = new HashSet<MeasurementData>();
            MeasurementLocations = new HashSet<MeasurementLocation>();
            Messages = new HashSet<Message>();
            Messages1 = new HashSet<Message>();
            Photos = new HashSet<Photo>();
            WarningSettings = new HashSet<WarningSetting>();
        }

        [Key]
        public int userID { get; set; }

        [StringLength(50)]
        public string userName { get; set; }

        public int roleID { get; set; }

        [Required]
        [StringLength(50)]
        public string loginName { get; set; }

        [Required]
        [StringLength(20)]
        public string loginPassword { get; set; }

        public DateTime? birthDate { get; set; }

        [StringLength(20)]
        public string phoneNumber { get; set; }

        [StringLength(20)]
        public string userPrivateNumber { get; set; }

        [StringLength(50)]
        public string emailAddress { get; set; }

        [StringLength(200)]
        public string address { get; set; }

        [StringLength(50)]
        public string userPhoto { get; set; }

        public int userStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeasurementData> MeasurementDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeasurementLocation> MeasurementLocations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Photo> Photos { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WarningSetting> WarningSettings { get; set; }
    }
}
