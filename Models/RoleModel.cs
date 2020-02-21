using Models.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RoleModel
    {
        private LaichauDBContext context = null;
        public RoleModel()
        {
            context = new LaichauDBContext();
        }
        public bool DeleteRole(int roleID)
        {
            bool res = false;
            res = context.Database.SqlQuery<bool>("sp_Delete_Role @RoleID", new SqlParameter("@RoleID", roleID)).SingleOrDefault();
            return res;
        }
    }
}
