using manage.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace manage.Filter
{
    public class LoginFilter: IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //ActionDescriptor.ControllerDescriptor.ControllerName;
            //context.ActionDescriptor
            //string con=context.ActionDescriptor.ControllerDescriptor.ControllerName;

            if (context.Controller.ToString().Contains("LoginController"))
            {
                return;
            }
            else if (context.Controller.ToString().Contains("CategoryController"))
            {
                return;
            }
            else if (context.Controller.ToString().Contains("CommonController"))
            {
                return;
            }
            else
            {
                // do something before the action executes
                User loginuser = context.HttpContext.Session.GetObject<User>("loginuser");
                if (loginuser == null)
                {
                    context.Result = new RedirectResult("/Login");
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }
    }
}
