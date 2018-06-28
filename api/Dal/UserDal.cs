using Dapper;
using Model.Form;
using System;
using System.Collections.Generic;
using System.Text;
using Model.DB;
namespace Dal
{
    public class UserDal:DbBase
    {
        public User GetDetail(UserForm model)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("username", model.username);
            parameters.Add("pwd", model.pwd);
            string sqlstr = "select id,username from user where username=@username and pwd=@pwd";
            return GetDetail<User>(sqlstr, parameters);
        }
    }
}
