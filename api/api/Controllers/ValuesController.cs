using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Model;
using Microsoft.Extensions.Options;
using Common;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IOptions<WechatConfig> wechatconfig;
        private readonly string appid;
        private readonly string appsecret;
        public ValuesController(IOptions<WechatConfig> settings)
        {
            wechatconfig = settings;
            appid = wechatconfig.Value.appid;
            appsecret = wechatconfig.Value.appsecret;
        }
        // GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get(string code)
        //{
        //    string verifyurl = wechatconfig.Value.verifyurl.Replace("paramer0",appid).Replace("paramer1",appsecret).Replace("paramer2",code);

        //    return new string[] {verifyurl };
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get()
        {
            return Json(new { aa = MD5pwd.getmd5("123456") });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
