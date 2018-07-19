using Dapper;
using Model.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal
{
    public class CategoryDal:DbBase
    {
        public bool AddOrEdit(Model.DB.Category model)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@c_id", model.c_id);
            parameters.Add("@c_name", model.c_name);
            parameters.Add("@operate_by", model.operate_by);
            parameters.Add("@img_path", model.img_path);
            parameters.Add("@is_enable", model.is_enable);
            parameters.Add("@create_at", model.create_at);
            parameters.Add("@operate_at", model.operate_at);
            if (model.c_id == 0)
            {
                string sqlstr = "insert into category (c_name,operate_by,img_path,is_enable,create_at,operate_at) values (@c_name,@operate_by,@img_path,@is_enable,@create_at,@operate_at)";
                return AddOrEdit(sqlstr,parameters);
            }
            else
            {
                string sqlstr = "update category set c_name=@c_name,operate_by=@operate_by,img_path=@img_path,operate_at=@operate_at where c_id=@c_id";
                return AddOrEdit(sqlstr, parameters);
            }
            
        }
        public List<Category> GetList()
        {
            string sqlstr = "select * from category order by operate_at desc,is_enable asc";
            return GetList<Category>(sqlstr,null);
        }
    }
}
