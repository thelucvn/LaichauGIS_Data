using System.ComponentModel.DataAnnotations;

namespace LaichauGIS_Data.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required]
        public string LoginName { set; get; }
        public string LoginPassword { set; get; }
        public bool RememberMe { set; get; }

    }
}