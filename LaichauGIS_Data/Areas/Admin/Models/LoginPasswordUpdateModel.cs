using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaichauGIS_Data.Areas.Admin.Models
{
    public class LoginPasswordUpdateModel
    {
        [Key]
        public int userID { get; set; }
        [DisplayName("Mật khẩu hiện tại")]
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu hiện tại!")]
        public string oldPassword { get; set; }
        [DisplayName("Mật khẩu mới")]
        [Required(ErrorMessage="Bạn chưa nhập mật khẩu mới!")]
        public string newPassword { get; set; }
        [DisplayName("Nhập lại mật khẩu mới")]
        [Required(ErrorMessage = "Bạn chưa nhập mật lại khẩu mới!")]
        public string retypePassword { get; set; }
    }
}