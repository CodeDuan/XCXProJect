using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using Microsoft.Extensions.Options;
using api.Model;

namespace api.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
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
        [Route("api/User/Login")]
        public JsonResult login(string code)
        {
            string verifyurl = wechatconfig.Value.verifyurl.Replace("paramer0", appid).Replace("paramer1", appsecret).Replace("paramer2", code);
            WebRequest wReq = WebRequest.Create(verifyurl);
            WebResponse wResp = wReq.GetResponse();
            Stream respStream = wResp.GetResponseStream();
            StreamReader reader = new StreamReader(respStream);
            string res=reader.ReadToEnd();
            return Json(new { obj= res });
        }
    }
}