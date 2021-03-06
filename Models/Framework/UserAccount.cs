﻿namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("UserAccount")]
    public partial class UserAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserAccount()
        {
            MeasurementLocations = new HashSet<MeasurementLocation>();
            Messages = new HashSet<Message>();
            Photos = new HashSet<Photo>();
            WarningSettings = new HashSet<WarningSetting>();
        }
        [DisplayName("Phân loại")]
        [NotMapped]
        public string roleName { get; set; }
        [Key]
        public int userID { get; set; }

        [StringLength(50)]
        [DisplayName("Họ và Tên")]
        [Required(ErrorMessage ="Bạn chưa nhập Họ tên")]
        public string userName { get; set; }

        [DisplayName("Phân loại người dùng")]
        public int roleID { get; set; }

        [StringLength(50)]
        [DisplayName("Tên đăng nhập")]
        [Required(ErrorMessage = "Bạn chưa nhập tên đăng nhập")]
        public string loginName { get; set; }

        
        [StringLength(20)]
        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        public string loginPassword { get; set; }

        [DisplayName("Ngày sinh (MM/dd/yyyy)")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? birthDate { get; set; }

        [StringLength(20)]
        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage ="Bạn chưa nhập số điện thoại")]
        [DefaultValue("")]
        public string phoneNumber { get; set; }

        [StringLength(20)]
        [DisplayName("Mã thẻ căn cước")]
        public string userPrivateNumber { get; set; }

        [StringLength(50)]
        [DisplayName("Địa chỉ Email")]
        [Required(ErrorMessage ="Bạn chưa nhập Email")]
        public string emailAddress { get; set; }

        [StringLength(200)]
        [DisplayName("Địa chỉ cư trú")]
        [DefaultValue("")]
        public string address { get; set; }

        [StringLength(50)]
        [DisplayName("Ảnh chân dung")]
        [DefaultValue("")]
        public string userPhoto { get; set; }

        [DisplayName("Trạng thái hoạt động")]
        public int userStatus { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        [NotMapped]
        public DateTime SelectedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeasurementLocation> MeasurementLocations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Photo> Photos { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WarningSetting> WarningSettings { get; set; }
    }
}
