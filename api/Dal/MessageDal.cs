using Model.Form;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using System.Linq;
namespace Dal
{
    public class MessageDal:DbBase
    {
        public bool Add(FormMessage model)
        {
            using (IDbConnection conn = new MySqlConnection(connstr))
            {
                conn.Open();
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@category_id", model.category_id);
                    parameters.Add("@content", model.content);
                    parameters.Add("@wechatuser", model.wechatuser);
                    parameters.Add("@name", model.name);
                    parameters.Add("@phone", model.phone);
                    parameters.Add("@isaudit", false);
                    string sql = "insert into message (category_id,content,wechatuser,name,phone,isaudit,createdate) values (@category_id,@content,@wechatuser,@name,@phone,@isaudit,now()); ";
                    sql += " SELECT LAST_INSERT_ID();";
                    int id = conn.Query<int>(sql: sql, param: parameters, transaction: transaction).AsList<int>().FirstOrDefault();

                    string imgsql = "insert into message_img (message_id,path) values (@message_id,@path)";
                    List<object> pa = new List<object>();
                    foreach(var item in model.img_path)
                    {
                        pa.Add(new { message_id=id,path=item });
                    }
                    if (pa.Count > 0)
                    {
                        conn.Execute(sql: imgsql, param: pa, transaction: transaction);
                    }
                    transaction.Commit();
                    conn.Close();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    conn.Close();
                    throw;
                }
            }
        }
    }

}
