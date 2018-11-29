using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DB
{
    public class Message
    {
        public int id { get; set; }
        public int category_id { get; set; }
        public string content { get; set; }
        /// <summary>
        /// 微信用户名
        /// </summary>
        public string wechatuser { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string name { get; set; }
        public string phone { get; set; }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool isaudit { get; set; }
        public DateTime createdate { get; set; }
        /// <summary>
        /// 浏览人数
        /// </summary>
        public int views { get; set; }
        /// <summary>
        /// 点赞人数
        /// </summary>
        public int givelike { get; set; }
        /// <summary>
        /// open_id
        /// </summary>
        public string open_id { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string user_img { get; set; }
    }
    public class message_img
    {
        public int id { get; set; }
        public int message_id { set; get; }
        public string path { get; set; }
    }
    public class MessageForList : Message
    {
        public List<message_img> imgs { get; set; }
    }
}
