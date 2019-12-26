namespace Models.Framework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Photo")]
    public partial class Photo
    {
        public int photoID { get; set; }

        [StringLength(50)]
        public string photoTitle { get; set; }

        public int locationID { get; set; }

        public int uploadBy { get; set; }

        public DateTime uploadDate { get; set; }

        public int photoStatus { get; set; }

        [Required]
        [StringLength(200)]
        public string photoUrl { get; set; }

        public virtual MeasurementLocation MeasurementLocation { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}
