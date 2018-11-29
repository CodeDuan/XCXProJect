using Model.Form;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using System.Linq;
using Model.DB;

namespace Dal
{
    public class MessageDal:DbBase
    {
        /// <summary>
        /// 用户发布信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

                    if (model.img_path?.Count > 0)
                    {
                        string imgsql = "insert into message_img (message_id,path) values (@message_id,@path)";
                        List<object> pa = new List<object>();
                        foreach (var item in model.img_path)
                        {
                            pa.Add(new { message_id = id, path = item });
                        }
                        if (pa.Count > 0)
                        {
                            conn.Execute(sql: imgsql, param: pa, transaction: transaction);
                        }
                    }
                    transaction.Commit();
                    conn.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    conn.Close();
                    throw;
                }
            }
        }

        /// <summary>
        /// 获取分类信息列表
        /// </summary>
        /// <returns></returns>
        public List<MessageForList> GetList(int page_index, int page_size, int category,out int rowcount)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select id,content,wechatuser,name,phone,createdate,views,givelike,user_img from message ");
            sql.Append("where isaudit = 0 and category_id = @category ");
            sql.Append("order by createdate desc ");
            sql.Append("limit @limitrow,@page_size ");
            
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@page_index", page_index);
            parameters.Add("@page_size", page_size);
            parameters.Add("@limitrow", (page_index - 1) * page_size);
            parameters.Add("@category", category);

            var list=GetList<MessageForList>(sql.ToString(), parameters);
            if (list.Count > 0)
            {
                //以下获取信息图片
                List<int> ids = list.Select(x => x.id).ToList();
                string sqlimg = "select id,message_id,path from message_img where message_id in @ids";
                DynamicParameters paramimg = new DynamicParameters();
                paramimg.Add("@ids", ids.ToArray());
                var list_img = GetList<message_img>(sqlimg, paramimg);
                foreach (var item in list)
                {
                    item.imgs = list_img.Where(x => x.message_id == item.id).ToList();
                }
            }

            sql.Clear();
            sql.Append("select count(id) from message ");
            sql.Append("where isaudit = 0 and category_id = @category ");
            rowcount=GetDetail<int>(sql.ToString(), parameters);
            return list;
        }
    }

}
