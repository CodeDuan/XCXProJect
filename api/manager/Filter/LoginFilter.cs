using Microsoft.AspNetCore.Mvc.Filters;
using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using manager.Common;
using Microsoft.AspNetCore.Mvc;

namespace manager.Filter
{
    public class LoginFilter:IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // do something before the action executes
            User loginuser = context.HttpContext.Session.GetObject<User>("loginuser");
            if(loginuser==null)
            {
                context.Result= new RedirectResult("/login.html");
            }

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }
    }
}
