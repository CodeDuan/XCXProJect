using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Microsoft.AspNetCore.Mvc;
using Model.Form;
using Model.DB;
using Common;

namespace manager.Controllers
{
    public class UserController : Controller
    {
        public UserDal user_dal;
        public UserController()
        {
            user_dal = new UserDal();
        }
        
        [HttpPost]
        [Route("user/login")]
        public IActionResult login(UserForm model)
        {
            model.pwd = MD5pwd.getmd5(model.pwd);
            User loginuser = user_dal.GetDetail(model);

            if (loginuser != null)
            {

                return Json(new { res = 1, msg = "登陆成功" });
            }
            else
            {
                return Json(new { res = 0, msg = "登陆失败" });
            }
        }
        [HttpGet]
        public IActionResult test(string model)
        {
            return Json(new { aa = "sdf" });
        }


    }
}