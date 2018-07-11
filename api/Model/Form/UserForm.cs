using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Form
{
    public class UserForm
    {
        public string username { get; set; }
        public string pwd { get; set; }
        //private string _pwd;
        //public string pwd
        //{
        //    set
        //    {
        //        _pwd = value;
        //    }
        //    get
        //    {
        //        return MD5pwd.getmd5(pwd);
        //    }
        //}
        /// <summary>
        /// 答案
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// Cookie中对应Captcha的值
        /// </summary>
        public string Captcha { get; set; }
    }
}
