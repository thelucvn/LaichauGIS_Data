using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Framework
{
   public class MessageFirebase
    {
        public int userID { get; set; }
        public string userToken { get; set; }
        public int receivedEnable { get; set; }
        public int settingID { get; set; }
        public Nullable<int> locationID { get; set; }
    }
}
