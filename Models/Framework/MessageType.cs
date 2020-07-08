namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MessageType")]
    public partial class MessageType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MessageType()
        {
            Messages = new HashSet<Message>();
        }

        public int messageTypeID { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Loại thông báo")]
        public string messageTypeName { get; set; }

        [StringLength(200)]
        [DisplayName("Mô tả")]
        public string typeDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }
    }
}
