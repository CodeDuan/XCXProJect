using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace api.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public IHttpActionResult login(int id,string name)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
            (1,
                name,
                DateTime.Now,
                DateTime.Now.AddDays(1),
                true,
                "7,1,8",
                "/"
            );
            string token = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, token);
            cookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(cookie);
            return Json(new { res=token});
        }
        [HttpGet]
        [Route("api/User/logout")]
        public IHttpActionResult logout()
        {
            int res = 2;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                res = 1;
                cookie.Expires = DateTime.Now.AddDays(-7);
                cookie.Value = "1";
                HttpContext.Current.Response.AppendCookie(cookie);
            }
            return Json(new { res});
        }
        [HttpGet]
        [Route("api/User/Content")]
        public IHttpActionResult Content()
        {
            string s = HttpContext.Current.User.Identity.Name;
            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            string role = ticket.UserData;
            string name = ticket.Name;
            
            return Json(new { name,role,s});
        }
    }
}
