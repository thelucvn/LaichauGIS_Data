using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Framework
{
    [Table("PhotoAlbum")]
   public class PhotoAlbum
    {
        [Key]
        public int photoAlbumID { get; set; }
        [DisplayName("Tiêu đề")]
        [Required(ErrorMessage ="Bạn chưa nhập tiêu đề")]
        public string albumTitle { get; set; }
        [DisplayName("Địa điểm")]
        public int wardID { get; set; }
        public Nullable<int> locationID { get; set; }
        [DisplayName("Ngày đăng tải")]
        public DateTime createDate { get; set; }
        public int userID { get; set; }
        [DisplayName("Trạng thái")]
        public int albumStatus { get; set; }
        [DisplayName("Kinh độ")]
        public double latitude { get; set; }
        [DisplayName("Vĩ độ")]
        public double longitude { get; set; }
        [DisplayName("Đăng tải bởi")]
        public string userName { get; set; }
        [DisplayName("Địa điểm")]
        public string wardName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhotoInAlbum> PhotoInAlbums { get; set; }

    }
}
