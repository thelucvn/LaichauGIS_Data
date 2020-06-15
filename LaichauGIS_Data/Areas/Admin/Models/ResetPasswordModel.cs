using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaichauGIS_Data.Areas.Admin.Models
{
    public class ResetPasswordModel
    {
        [Key]
        [Required(ErrorMessage ="Bạn chưa nhập địa chỉ Email!")]
        public string emailAddress { get; set; }
    }
}