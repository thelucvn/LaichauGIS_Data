using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Framework
{
    [Table("PhotoInAlbum")]
    public class PhotoInAlbum
    {
        [Key]
        public int photoID { get; set; }
        public string photoUrl { get; set; }
        public int photoALbumID { get; set; }
        public string photoTitle { get; set; }
    }
}
