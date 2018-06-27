using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using api.Model;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;

namespace api.Controllers
{
    [Produces("application/json")]
    public class UserController : Controller
    {
        private readonly IOptions<WechatConfig> wechatconfig;
        private readonly string appid;
        private readonly string appsecret;
        public UserController(IOptions<WechatConfig> settings)
        {
            wechatconfig = settings;
            appid = wechatconfig.Value.appid;
            appsecret = wechatconfig.Value.appsecret;
        }
        [Route("api/user/login")]
        public JsonResult login(string code)
        {
            string verifyurl = wechatconfig.Value.verifyurl.Replace("paramer0", appid).Replace("paramer1", appsecret).Replace("paramer2", code);
            WebRequest wReq = WebRequest.Create(verifyurl);
            WebResponse wResp = wReq.GetResponse();
            Stream respStream = wResp.GetResponseStream();
            StreamReader reader = new StreamReader(respStream);
            string res = reader.ReadToEnd();
            return Json(new { obj = res });
        }
        [Route("api/User/test")]
        public JsonResult Test()
        {
            string verifyurl = "adfadf";
            return Json(new { verifyurl });
        }
        #region 登陆demo
        //[HttpPost]
        //[Route("User/Login")]
        //public JsonResult Login(User model)
        //{
        //    User user = new User { username = "duan", password = "111" };
        //    string secretKey = "encrypt_the_validate_site_key";
        //    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        //    TokenGenerateOption _Option = new TokenGenerateOption
        //    {
        //        Path = "/Login",
        //        Audience = "http://www.duanby.com",
        //        Issuer = "http://www.duanby.com",
        //        SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
        //        Expiration = TimeSpan.FromMinutes(15)
        //    };
        //    if (model.username == user.username && model.password == user.password)
        //    {
        //        var now = DateTime.UtcNow;
        //        var claims = new Claim[]
        //         {
        //             new Claim(JwtRegisteredClaimNames.Sub, model.userid.ToString()),
        //             new Claim(JwtRegisteredClaimNames.UniqueName, model.username),
        //             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //             new Claim(JwtRegisteredClaimNames.Iat, now.ToString(), ClaimValueTypes.Integer64)
        //         };

        //        var jwt = new JwtSecurityToken(_Option.Issuer, _Option.Audience, claims, now, now.Add(_Option.Expiration), _Option.SigningCredentials);
        //        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        //        return Json(new { reg = 1 });
        //    }
        //    else
        //    {
        //        return Json(new { reg = 0,msg="用户名或密码错误" });
        //    }
        //}
        #endregion
    }
}