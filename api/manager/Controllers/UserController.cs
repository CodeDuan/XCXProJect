using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Microsoft.AspNetCore.Mvc;
using Model.Form;
using Model.DB;
using Common;
using manager.Common;
using Microsoft.AspNetCore.Http;

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
            try
            {
                model.pwd = MD5pwd.getmd5(model.pwd);
                User loginuser = user_dal.GetDetail(model);

                if (loginuser != null)
                {
                    HttpContext.Session.SetObject<User>("loginuser", loginuser);
                    Response.Cookies.Append("loginuser", loginuser.username);//前端用此cookie验证登陆状态
                    return Json(new { Code = 100, Message = "登陆成功" });
                }
                else
                {
                    return Json(new { Code = 101, Message = "用户名或密码错误" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Code = 200, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("user/verifycode")]
        public async Task<ActionResult> verifycode()
        {
            var model = await CaptchaFactory.Intance.CreateAsync();
            Response.Cookies.Append("Captcha", model.Answer);
            return File(model.Image, model.ContentType);
        }
        [HttpPost, Route("user/verify")]
        public async Task<ActionResult> verify(VerifyRequest model)
        {
            try
            {
                var response = await CaptchaFactory.Intance.VerifyAsync(model);
                return Json(new { response,model=model });
            }
            catch (Exception ex)
            {
                return Json(new { res=ex.Message,model = model });
            }
        }
    }
}