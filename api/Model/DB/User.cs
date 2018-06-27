using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DB
{
    public class User
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string pwd { get; set; }
        /// <summary>
        /// 登陆ip
        /// </summary>
        public string loginip { get; set; }
        /// <summary>
        /// 登陆时间
        /// </summary>
        public DateTime logintime { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime addtime { get; set; }

    }
}
