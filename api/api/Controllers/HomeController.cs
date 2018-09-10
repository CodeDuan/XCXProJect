using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace api.Controllers
{
    [Produces("application/json")]
    public class HomeController : Controller
    {
        
        private Dal.CategoryDal category_dal;
        public HomeController(IOptions<WechatConfig> settings)
        {
            category_dal = new Dal.CategoryDal();
        }
        /// <summary>
        /// 获取开启的分类
        /// </summary>
        /// <returns></returns>
        [Route("api/home/getgategory")]
        public IActionResult GetGategory()
        {
            var list= category_dal.GetList();
            return Json(new { gategory_list = list });
        }
    }
}