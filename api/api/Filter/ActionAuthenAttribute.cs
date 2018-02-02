using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace api.Filter
{
    public class ActionAuthenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var id = HttpContext.Current.User.Identity.Name;
            if (string.IsNullOrEmpty(id))
            {
                actionContext.Response = actionContext.Request
                   .CreateResponse(HttpStatusCode.OK, new Result()
                   {
                       Code = ResultCode.NoAuthen,
                       Message = "没有权限"
                   }, "application/json");
            }
            else //通过验证
            {
                return;
            }
        }
    }
}