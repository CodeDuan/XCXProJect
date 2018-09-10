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
using SkiaSharp;

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
                return Json(new {Response= new VerifyResponse() { Code = 100, Message = "验证成功" },model=model }); 
                //var response = await CaptchaFactory.Intance.VerifyAsync(model);
                //return Json(new { response, model = model });
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

        /// <summary>
        /// 获取验证码的另一种方式
        /// </summary>
        /// <returns></returns>
        public IActionResult Code()
        {
            #region 反射SK支持的全部颜色
            //List<SKColor> colors = new List<SKColor>();           
            //var skcolors = new SKColors();
            //var type = skcolors.GetType();
            //foreach (FieldInfo field in type.GetFields())
            //{
            //    colors.Add( (SKColor)field.GetValue(skcolors));
            //}
            #endregion

            //int maxcolorindex = colors.Count-1;
            string text = "1A3V";
            var zu = text.ToList();
            SKBitmap bmp = new SKBitmap(80, 30);
            using (SKCanvas canvas = new SKCanvas(bmp))
            {
                //背景色
                canvas.DrawColor(SKColors.White);

                using (SKPaint sKPaint = new SKPaint())
                {
                    sKPaint.TextSize = 16;//字体大小
                    sKPaint.IsAntialias = true;//开启抗锯齿                   
                    sKPaint.Typeface = SKTypeface.FromFamilyName("微软雅黑", SKTypefaceStyle.Bold);//字体
                    SKRect size = new SKRect();
                    sKPaint.MeasureText(zu[0].ToString(), ref size);//计算文字宽度以及高度

                    float temp = (bmp.Width / 4 - size.Size.Width) / 2;
                    float temp1 = bmp.Height - (bmp.Height - size.Size.Height) / 2;
                    Random random = new Random();

                    for (int i = 0; i < 4; i++)
                    {

                        sKPaint.Color = new SKColor((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
                        canvas.DrawText(zu[i].ToString(), temp + 20 * i, temp1, sKPaint);//画文字
                    }
                    //干扰线
                    for (int i = 0; i < 5; i++)
                    {
                        sKPaint.Color = new SKColor((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
                        canvas.DrawLine(random.Next(0, 40), random.Next(1, 29), random.Next(41, 80), random.Next(1, 29), sKPaint);
                    }
                }
                //页面展示图片
                using (SKImage img = SKImage.FromBitmap(bmp))
                {
                    using (SKData p = img.Encode())
                    {
                        return File(p.ToArray(), "image/Png");
                    }
                }
            }
        }
    }
}