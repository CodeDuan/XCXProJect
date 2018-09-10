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
            parameters.Add("@create_at", DateTime.Now.ToShortDateString());
            parameters.Add("@operate_at", DateTime.Now.ToShortDateString());
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
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<Category> GetList()
        {
            string sqlstr = "select c_id,c_name,operate_by,img_path,is_enable,create_at,operate_at from category order by operate_at desc,is_enable asc";
            return GetList<Category>(sqlstr,null);
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="c_id"></param>
        /// <returns></returns>
        public Category GetDetail(int c_id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@c_id", c_id);
            string sqlstr = "select c_id, c_name, operate_by, img_path, is_enable, create_at, operate_at from category";
            return GetDetail<Category>(sqlstr, parameters);
        }
        /// <summary>
        /// 更改分类状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="c_id"></param>
        /// <returns></returns>
        public bool changestatus(bool is_enable, int c_id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@c_id", c_id);
            parameters.Add("@is_enable", is_enable);
            parameters.Add("@now",DateTime.Now.ToShortDateString());
            string sqlstr = "update category set is_enable=@is_enable,operate_at=@now where c_id=@c_id ";
            return AddOrEdit(sqlstr, parameters);
        }
    }
}
