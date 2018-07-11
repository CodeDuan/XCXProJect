using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace manage.Controllers
{
    public class CategoryController : Controller
    {
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
        public IActionResult AddOrEdit()
        {
            return View();
        }

    }
}