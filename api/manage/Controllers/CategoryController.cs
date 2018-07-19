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
        public IActionResult Detail(int c_id)
        {
            return View(new Model.DB.Category());
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
        public IActionResult GetList()
        {
            return Json(new { Code=200, result = category_dal.GetList() }) ;
        }
    }
}