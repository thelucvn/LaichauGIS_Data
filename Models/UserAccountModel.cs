using Models.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using PagedList;
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
        public IEnumerable<UserAccount> ListAll(int page,int pageSize)
        {
            var list = context.Database.SqlQuery<UserAccount>("sp_UserAccount_ListAll").ToList().OrderByDescending(x=>x.userID).ToPagedList(page,pageSize);
            return list;
        }
        public List<UserAccount> ListProvider()
        {
            var list = context.Database.SqlQuery<UserAccount>("sp_UserAccount_ListProvider").ToList();
            return list;
        }
        public UserAccount getUserAccountByID(int id)
        {
            object[] parameters = {
            new SqlParameter("@UserID",id)
            };
            var res = context.Database.SqlQuery<UserAccount>("sp_UserAccount_GetUserbyID @UserID", parameters).SingleOrDefault();
            return res;
        }
        public bool UpdateUserAccount(UserAccount userEntity)
        {
            string date = userEntity.birthDate.ToString();
            DateTime? birthDate = Convert.ToDateTime(date);
            string userPrivateNumber = userEntity.userPrivateNumber ?? " ";
            string address = userEntity.address ?? " ";
            string userPhoto = userEntity.userPhoto ?? " ";
                SqlParameter[] parameters =new SqlParameter[]
                {
                    new SqlParameter("@UserID",userEntity.userID),
                    new SqlParameter("@UserName",userEntity.userName),
                    new SqlParameter("@BirthDate",birthDate),
                    new SqlParameter("@PhoneNumber",userEntity.phoneNumber),
                    new SqlParameter("@UserPrivateNumber",userPrivateNumber),
                    new SqlParameter("@EmailAddress",userEntity.emailAddress),
                    new SqlParameter("@Address",address),
                    new SqlParameter("@UserPhoto",userPhoto)
                };
               var res=context.Database.SqlQuery<bool>("sp_Update_UserAccount @UserID,@UserName,@BirthDate,@PhoneNumber,@UserPrivateNumber,@EmailAddress,@Address,@UserPhoto", parameters).SingleOrDefault();
               return res;

        }
        public bool DeleteUserAccount(int id)
        {
            var res = context.Database.SqlQuery<bool>("sp_Delete_UserAccount @UserID", new SqlParameter("@UserID", id)).SingleOrDefault();
            return res;
        }

    }
}
