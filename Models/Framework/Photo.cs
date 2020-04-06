using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Photo")]
    public partial class Photo
    {
        public int photoID { get; set; }

        [StringLength(200)]
        [DisplayName("Tiêu đề")]
        [Required(ErrorMessage = "Bạn chưa nhập Tiêu đề!")]
        public string photoTitle { get; set; }

        [DisplayName("Địa điểm")]
        public int wardID { get; set; }
        public Int32? locationID { get; set; }

        [DisplayName("Upload by")]
        public int uploadBy { get; set; }

        [DisplayName("Ngày đăng")]
        public DateTime uploadDate { get; set; }

        [DisplayName("Trạng thái")]
        public int photoStatus { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public string photoUrl { get; set; }

        public virtual Ward Ward { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}
