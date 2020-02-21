namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Role")]
    public partial class Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Role()
        {
            UserAccounts = new HashSet<UserAccount>();
        }

        public int roleID { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Loại tài khoản")]
        public string roleName { get; set; }

        [StringLength(100)]
        [DisplayName("Mô tả")]
        public string descriptionInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
