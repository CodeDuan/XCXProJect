using api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace api.Filter
{
    public class ErroLogFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext filterContext)
        {
            if (null != filterContext.Exception)
            {
                string controllerName = filterContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                string actionName = filterContext.ActionContext.ActionDescriptor.ActionName;
                var paramss = filterContext.ActionContext.ActionArguments;
                string param = "";
                if (filterContext.Request.Method.Method.ToLower() == "post")
                {
                    param = paramss == null ? "" : JsonConvert.SerializeObject(paramss.Values);
                }
                else
                {
                    param = paramss == null ? "" : JsonConvert.SerializeObject(paramss);
                }
                string strMessage = string.Format("控制器:{0}；:action:{1}；param:{2}；错误时间：{3}", controllerName, actionName, param, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                string a = filterContext.Exception.Message;
                HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                Result res = new Result() { Code = ResultCode.ServerError, Message = filterContext.Exception.Message, Data=null };
                responseMessage.Content = new StringContent(JsonConvert.SerializeObject(res), Encoding.UTF8, "text/json");
                filterContext.Response = responseMessage;
            }
        }
    }
}