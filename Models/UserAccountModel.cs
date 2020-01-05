using Models.Framework;
using System.Collections.Generic;
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
        public bool Login(string loginName, string loginPassword)
        {
            object[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@LoginName",loginName),
                new SqlParameter("@LoginPassword",loginPassword)
            };
            var res = context.Database.SqlQuery<bool>("sp_User_Login @LoginName,@LoginPassword", sqlParams).SingleOrDefault();
            return res;
        }
        public int Create(string username, string loginName, string loginPassword, string phoneNumber, string emailAddress) {
            object[] parameters = {
            new SqlParameter("@UserName",username),
            new SqlParameter("@LoginName", loginName),
            new SqlParameter("@LoginPassword", loginPassword),
            new SqlParameter("@PhoneNumber", phoneNumber),
            new SqlParameter("@EmailAddress", emailAddress)
        };
            var res = context.Database.SqlQuery<int>("sp_Create_UserAccount @UserName,@LoginName,@LoginPassword,@PhoneNumber,@EmailAddress", parameters).SingleOrDefault();
            return res;
        }
        public List<UserAccount> ListAll()
        {
            var list = context.Database.SqlQuery<UserAccount>("sp_UserAccount_ListAll").ToList();
            return list;
        }

    }
}
