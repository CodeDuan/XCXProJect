using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Form;
using api.Common;
using System.Text;

namespace api.Controllers
{

    public class MessageController : ControllerBase
    {
        private MessageDal message_dal;
        public MessageController()
        {
            message_dal = new MessageDal();
        }
        /// <summary>
        /// 用户提交信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost,Route("api/message/addusermessage")]
        public IActionResult AddUserMessage([FromBody]FormMessage model)
        {
            var errMsg = new StringBuilder();
            if (model.name.Length == 0)
            {
                errMsg.Append("联系人不能为空");
            }
            if (model.phone.Length == 0)
            {
                errMsg.Append("联系电话不能为空");
            }
            if (model.content.Length == 0 && model.img_path.Count == 0)
            {
                errMsg.Append("内容和图片不能同时为空");
            }
            if(errMsg.Length>0)
            {
                return new JsonResult(new { res = 0,msg=errMsg.ToString() });
            }
            if (!message_dal.Add(model))
            {
                return new JsonResult(new { res = 0, msg = "" });
            }
            else
            {
                return new JsonResult(new { res = 1, msg = "" });
            }
        }
    }
}