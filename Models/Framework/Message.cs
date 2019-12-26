namespace Models.Framework
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Message")]
    public partial class Message
    {
        public int messageID { get; set; }

        public int senderID { get; set; }

        public int reiceiverID { get; set; }

        public int messageTypeID { get; set; }

        [Required]
        [StringLength(200)]
        public string messageTitle { get; set; }

        [Required]
        public string messageContent { get; set; }

        public int messageStatus { get; set; }

        public virtual MessageType MessageType { get; set; }

        public virtual UserAccount UserAccount { get; set; }

        public virtual UserAccount UserAccount1 { get; set; }
    }
}
