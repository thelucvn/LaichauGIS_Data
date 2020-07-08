namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        public int messageID { get; set; }

        public int senderID { get; set; }

        public int reiceiverID { get; set; }

        public int messageTypeID { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Tiêu đề")]
        public string messageTitle { get; set; }

        [Required]
        [DisplayName("Nội dung")]
        public string messageContent { get; set; }
        [DisplayName("Trạng thái")]
        public int messageStatus { get; set; }

        public virtual MessageType MessageType { get; set; }

        public virtual UserAccount UserAccount { get; set; }

        public virtual Ward Ward { get; set; }
    }
}
