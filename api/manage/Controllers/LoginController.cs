using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Dal;
using manage.Common;
using Microsoft.AspNetCore.Mvc;
using Model.DB;
using Model.Form;
namespace manage.Controllers
{
    public class LoginController : Controller
    {
        public UserDal user_dal;
        public LoginController()
        {
            user_dal = new UserDal();
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> verifycode()
        {
            var model = await CaptchaFactory.Intance.CreateAsync();
            Response.Cookies.Append("Captcha", model.Answer);
            return File(model.Image, model.ContentType);
        }
        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ActionResult> verify(VerifyRequest model)
        {
            try
            {
                var response = await CaptchaFactory.Intance.VerifyAsync(model);
                return Json(new { response, model = model });
            }
            catch (Exception ex)
            {
                return Json(new { res = ex.Message, model = model });
            }
        }
        /// <summary>
        /// 登陆方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> Login(UserForm model)
        {
            VerifyResponse res = await CaptchaFactory.Intance.VerifyAsync(new VerifyRequest() { Answer=model.Answer,Captcha=model.Captcha});
            if (res.Code != 100)
            {
                return Json(res) ;
            }
            model.pwd = MD5pwd.getmd5(model.pwd);
            User loginuser = user_dal.GetDetail(model);

            if (loginuser != null)
            {
                try
                {
                    HttpContext.Session.SetObject<User>("loginuser", loginuser);
                    return Json(new { Code = 100, Message = "登陆成功" });
                }
                catch(Exception ex) {
                    return Json(new { Code = 500, Message = ex.Message});
                }
            }
            else
            {
                return Json(new { Code = 101, Message = "用户名或密码错误" });
            }
        }
    }
}