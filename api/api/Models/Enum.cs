using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.Models
{
    public enum ResultCode
    {
        /// <summary>
        ///  登录相关错误信息
        /// </summary>
        NoLogged = 0,
        /// <summary>
        ///  处理成功
        /// </summary>
        OK = 200,
        /// <summary>
        ///  未授权
        /// </summary>
        Permissions = 401,
        /// <summary>
        /// 没有权限
        /// </summary>
        NoAuthen = 403,
        /// <summary>
        /// 数据验证失败
        /// </summary>
        NoValidate = 400,
        /// <summary>
        ///  服务器错误
        /// </summary>
        ServerError = 500
    }
}