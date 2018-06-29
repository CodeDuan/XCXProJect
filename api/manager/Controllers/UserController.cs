using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Microsoft.AspNetCore.Mvc;
using Model.Form;
using Model.DB;
using Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Model;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication;

namespace manager.Controllers
{
    public class UserController : Controller
    {
        private UserDal user_dal;
        public UserController()
        {
            user_dal = new UserDal();
        }
        
        [HttpPost]
        [Route("user/login")]
        public IActionResult login(UserForm model)
        {
            model.pwd= MD5pwd.getmd5(model.pwd);
            User loginuser = user_dal.GetDetail(model);
            if (loginuser != null)
            {
                var now = DateTime.UtcNow;
                var claims = new Claim[]
                {
                     new Claim(JwtRegisteredClaimNames.Sub, loginuser.id.ToString()),
                     new Claim(JwtRegisteredClaimNames.UniqueName, loginuser.username),
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                     new Claim(JwtRegisteredClaimNames.Iat, now.ToString(), ClaimValueTypes.Integer64)
                };
                //string secretKey = "encrypt_the_validate_site_key";
                //var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                //var _Option = new TokenGenerateOption
                //{
                //    Path = "/token",
                //    Audience = "www.duanby.com",
                //    Issuer = "www.duanby.com",
                //    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                //    Expiration = TimeSpan.FromMinutes(15),
                //};
                //var jwt = new JwtSecurityToken(_Option.Issuer, _Option.Audience, claims, now, now.Add(_Option.Expiration), _Option.SigningCredentials);
                //var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
 
                return Json(new { res = 1, msg = "登陆成功" });
            }
            else
            {
                return Json(new { res = 0, msg = "登陆失败" });
            }
        }
    }
}