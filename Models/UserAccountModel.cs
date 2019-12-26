using Models.Framework;
using System.Data.SqlClient;
using System.Linq;

namespace Models
{
    public class UserAccountModel
    {
        private LaichauDBContext context = null;
        public UserAccountModel()
        {
            context = new LaichauDBContext();
        }
        public int Login(string loginName, string loginPassword)
        {
            object[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@LoginName",loginName),
                new SqlParameter("@LoginPassword",loginPassword)
            };
            var res = context.Database.SqlQuery<int>("sp_User_Login @LoginName,@LoginPassword", sqlParams).SingleOrDefault();
            return res;
        }

    }
}
