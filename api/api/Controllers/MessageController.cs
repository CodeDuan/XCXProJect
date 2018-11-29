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
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
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
                return this.CreateResult(null, res: 0, msg: errMsg.ToString());
            }
            if (!message_dal.Add(model))
            {
                return this.CreateResult(null,res:0);
            }
            else
            {
                return this.CreateResult(null);
            }
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="page_index"></param>
        /// <param name="page_size"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet, Route("api/message/getmessage")]
        public IActionResult GetMessage(int page_index, int page_size, int category)
        {
            int rowcount;
            var listres=message_dal.GetList(page_index,page_size,category,out rowcount);
            int allpage = rowcount / page_size + 1;
            var list = listres.Select(x => new {
                x.id,
                x.user_img,
                x.wechatuser,
                x.phone,
                x.content,
                createdate = x.createdate.ToString("yyyy-MM-dd HH:mm"),
                x.views,
                x.givelike,
                img = x.imgs.Select(p=>p.path),
            });
            return new JsonResult(new { list,allpage });
        }
    }
}