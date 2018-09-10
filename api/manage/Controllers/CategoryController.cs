using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using manage.Common;
using Microsoft.AspNetCore.Mvc;
using Model.DB;

namespace manage.Controllers
{
    public class CategoryController : Controller
    {
        CategoryDal category_dal;
        public CategoryController()
        {
            category_dal = new CategoryDal();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="c_id"></param>
        /// <returns></returns>
        public IActionResult Detail(int? c_id)
        {
            if (c_id == null)
            {
                return View(new Model.DB.Category());
            }
            else
            {
                var detail = category_dal.GetDetail(c_id.Value);
                return View(detail);
            }
        }
        /// <summary>
        /// 添加/编辑
        /// </summary>
        /// <returns></returns>
        public IActionResult AddOrEdit(Model.DB.Category model)
        {
            User loginuser = HttpContext.Session.GetObject<User>("loginuser");
            model.operate_by = loginuser.username;
            category_dal.AddOrEdit(model);
            return Json(new { Code=200});
        }
        [HttpGet,Route("category/getlist")]
        public IActionResult GetList()
        {
            return Json(new { Code=200, data = category_dal.GetList() }) ;
        }
        [HttpPost, Route("category/changestatus")]
        public IActionResult changestatus(Model.DB.Category model)
        {
            if (model.c_id == 0)
            {
                return Json(new { Code = 100, res = "id不能为0"});
            }
            else
            {
                return Json(new { Code = 200, res = category_dal.changestatus(model.is_enable, model.c_id) });
            }
            
        }
    }
}